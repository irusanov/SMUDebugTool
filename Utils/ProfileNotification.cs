using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ZenStatesDebugTool
{
    /// <summary>
    /// Shows a notification when the saved profile has been applied. The startup/resume task runs as
    /// SYSTEM in session 0, which has no desktop, so the notification is shown by a short-lived helper
    /// process started in the session of the logged-on user.
    /// </summary>
    internal static class ProfileNotification
    {
        private const string NotifyArgumentPrefix = "/notify:";
        private const string WarningArgument = "/warning";
        private const string Title = "SMUDebugTool";

        // Marks the helper as "not an application instance" for the driver cleanup check. Global,
        // because the helper runs in another session than the process that checks for it.
        internal const string HelperMarkerPrefix = "Global\\ZenTimings.DriverCleanup.Notify.";

        private const uint InvalidSession = 0xFFFFFFFF;
        private const int TokenElevationType = 18;
        private const int TokenLinkedToken = 19;
        private const int TokenElevationTypeLimited = 3;
        private const uint MAXIMUM_ALLOWED = 0x02000000;
        private const int SecurityImpersonation = 2;
        private const int TokenPrimary = 1;
        private const uint CREATE_UNICODE_ENVIRONMENT = 0x00000400;
        private const uint CREATE_NO_WINDOW = 0x08000000;

        // The shell drops balloons while the desktop is still loading after logon, or when the user is
        // not accepting notifications. Wait for it, but not forever.
        private const int QUNS_ACCEPTS_NOTIFICATIONS = 5;
        private const int AcceptNotificationsTimeoutMs = 90000;
        private const int AcceptNotificationsPollMs = 500;
        private const int SettleMs = 1500;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [DllImport("kernel32.dll")]
        private static extern uint WTSGetActiveConsoleSessionId();

        [DllImport("wtsapi32.dll", SetLastError = true)]
        private static extern bool WTSQueryUserToken(uint sessionId, out IntPtr token);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool GetTokenInformation(IntPtr token, int informationClass, IntPtr buffer, int length, out int returnLength);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool DuplicateTokenEx(IntPtr existingToken, uint desiredAccess, IntPtr attributes, int impersonationLevel, int tokenType, out IntPtr newToken);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CreateProcessAsUser(IntPtr token, string applicationName, StringBuilder commandLine, IntPtr processAttributes, IntPtr threadAttributes,
            bool inheritHandles, uint creationFlags, IntPtr environment, string currentDirectory, ref STARTUPINFO startupInfo, out PROCESS_INFORMATION processInformation);

        [DllImport("userenv.dll", SetLastError = true)]
        private static extern bool CreateEnvironmentBlock(out IntPtr environment, IntPtr token, bool inherit);

        [DllImport("userenv.dll", SetLastError = true)]
        private static extern bool DestroyEnvironmentBlock(IntPtr environment);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("shell32.dll")]
        private static extern int SHQueryUserNotificationState(out int state);

        internal static bool IsNotifyArgument(string arg) =>
            arg.StartsWith(NotifyArgumentPrefix, StringComparison.OrdinalIgnoreCase);

        /// <summary>Entry point of the helper process: shows the notification and waits for it to close.</summary>
        internal static void RunNotifyProcess(string[] args)
        {
            string message = null;
            bool warning = false;

            foreach (string arg in args)
            {
                if (IsNotifyArgument(arg))
                {
                    try
                    {
                        message = Encoding.UTF8.GetString(Convert.FromBase64String(arg.Substring(NotifyArgumentPrefix.Length)));
                    }
                    catch (FormatException)
                    {
                    }
                }
                else if (string.Equals(arg, WarningArgument, StringComparison.OrdinalIgnoreCase))
                {
                    warning = true;
                }
            }

            if (string.IsNullOrEmpty(message))
                return;

            Mutex marker = null;

            try
            {
                using (Process current = Process.GetCurrentProcess())
                    marker = new Mutex(false, HelperMarkerPrefix + current.Id);
            }
            catch
            {
            }

            WaitUntilNotificationsAccepted();

            DriverCleanerNotification.Show(Title, message, warning);
            DriverCleanerNotification.WaitForPending();

            GC.KeepAlive(marker);
        }

        private static void WaitUntilNotificationsAccepted()
        {
            try
            {
                int state;
                int waited = 0;

                while (waited < AcceptNotificationsTimeoutMs)
                {
                    if (SHQueryUserNotificationState(out state) == 0 && state == QUNS_ACCEPTS_NOTIFICATIONS)
                    {
                        // Give the shell a moment to finish setting up the notification area.
                        Thread.Sleep(SettleMs);
                        return;
                    }

                    Thread.Sleep(AcceptNotificationsPollMs);
                    waited += AcceptNotificationsPollMs;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Shows the notification in the session of the logged-on user. Does nothing when nobody is
        /// logged on. Failure to notify must never affect applying the profile.
        /// </summary>
        internal static void ShowInUserSession(string message, bool warning = false)
        {
            IntPtr userToken = IntPtr.Zero;
            IntPtr token = IntPtr.Zero;
            IntPtr environment = IntPtr.Zero;
            PROCESS_INFORMATION process = new PROCESS_INFORMATION();

            try
            {
                uint session = WTSGetActiveConsoleSessionId();

                if (session == InvalidSession || !WTSQueryUserToken(session, out userToken))
                    return;

                // The executable requires administrator rights: use the elevated token of the user, if there is one.
                token = GetElevatedToken(userToken);

                if (token == IntPtr.Zero)
                    return;

                if (!CreateEnvironmentBlock(out environment, token, false))
                    environment = IntPtr.Zero;

                string exePath;
                using (Process current = Process.GetCurrentProcess())
                    exePath = current.MainModule.FileName;

                StringBuilder commandLine = new StringBuilder();
                commandLine.Append('"').Append(exePath).Append("\" ").Append(NotifyArgumentPrefix)
                    .Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(message)));

                if (warning)
                    commandLine.Append(' ').Append(WarningArgument);

                STARTUPINFO startupInfo = new STARTUPINFO();
                startupInfo.cb = Marshal.SizeOf(typeof(STARTUPINFO));
                startupInfo.lpDesktop = "winsta0\\default";

                if (CreateProcessAsUser(token, exePath, commandLine, IntPtr.Zero, IntPtr.Zero, false,
                    CREATE_UNICODE_ENVIRONMENT | CREATE_NO_WINDOW, environment, null, ref startupInfo, out process))
                {
                    CloseHandle(process.hThread);
                    CloseHandle(process.hProcess);
                }
            }
            catch
            {
            }
            finally
            {
                if (environment != IntPtr.Zero)
                    DestroyEnvironmentBlock(environment);

                if (token != IntPtr.Zero && token != userToken)
                    CloseHandle(token);

                if (userToken != IntPtr.Zero)
                    CloseHandle(userToken);
            }
        }

        // Returns a primary token for the user: the elevated one of an administrator running with UAC,
        // otherwise the token itself. The caller must close it unless it is the token passed in.
        private static IntPtr GetElevatedToken(IntPtr userToken)
        {
            IntPtr buffer = Marshal.AllocHGlobal(IntPtr.Size);

            try
            {
                int returned;

                if (!GetTokenInformation(userToken, TokenElevationType, buffer, 4, out returned)
                    || Marshal.ReadInt32(buffer) != TokenElevationTypeLimited)
                    return userToken;

                if (!GetTokenInformation(userToken, TokenLinkedToken, buffer, IntPtr.Size, out returned))
                    return userToken;

                IntPtr linked = Marshal.ReadIntPtr(buffer);
                IntPtr primary;
                bool duplicated = DuplicateTokenEx(linked, MAXIMUM_ALLOWED, IntPtr.Zero, SecurityImpersonation, TokenPrimary, out primary);

                CloseHandle(linked);

                return duplicated ? primary : userToken;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
    }
}
