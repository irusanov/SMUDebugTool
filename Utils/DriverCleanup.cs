using System;
using System.Diagnostics;
using System.Threading;
using static ZenStatesDebugTool.DriverCleaner;

namespace ZenStatesDebugTool
{
    /// <summary>
    /// Removes the inpoutx64 driver when the last application using it exits. The mutex names are shared
    /// with ZenTimings so both apps serialize their cleanups and neither pulls the driver out from under
    /// the other.
    /// </summary>
    internal static class DriverCleanup
    {
        internal const string CleanupArgument = "/driver-cleanup";
        private const string NotificationsArgumentPrefix = "/notifications:";

        private const string cleanupMutexName = "Local\\ZenTimings.DriverCleanup";

        // Held by each cleanup-mode process so it isn't counted as a running app instance.
        private const string cleanupProcessMarkerPrefix = "Local\\ZenTimings.DriverCleanup.Process.";

        // Processes that share the inpoutx64 driver.
        private static readonly string[] driverUsers = { "SMUDebugTool", "ZenTimings" };

        private static Mutex cleanupProcessMarker;

        internal static bool IsDriverCleanupMode { get; private set; }

        internal static bool IsCleanupArgument(string arg) =>
            string.Equals(arg, CleanupArgument, StringComparison.OrdinalIgnoreCase);

        internal static void RunCleanupProcess(string[] args)
        {
            IsDriverCleanupMode = true;

            using (Process current = Process.GetCurrentProcess())
                cleanupProcessMarker = new Mutex(false, cleanupProcessMarkerPrefix + current.Id);

            NotificationLevel notificationLevel = GetNotificationLevel(args);

            using (Mutex cleanupMutex = new Mutex(false, cleanupMutexName))
            {
                AcquireMutex(cleanupMutex);

                try
                {
                    DriverCleaner.Cleanup(notificationLevel);
                }
                finally
                {
                    cleanupMutex.ReleaseMutex();
                }
            }

            // Keep the notification alive only after the mutex is released, so a new instance isn't blocked.
            DriverCleanerNotification.WaitForPending();

            GC.KeepAlive(cleanupProcessMarker);
        }

        private static NotificationLevel GetNotificationLevel(string[] args)
        {
            foreach (string arg in args)
            {
                if (!arg.StartsWith(NotificationsArgumentPrefix, StringComparison.OrdinalIgnoreCase))
                    continue;

                string value = arg.Substring(NotificationsArgumentPrefix.Length);
                NotificationLevel level;
                if (Enum.TryParse(value, true, out level))
                    return level;
            }

            return NotificationLevel.All;
        }

        /// <summary>Blocks while a cleanup process (of either app) is removing the driver.</summary>
        internal static void WaitForDriverCleanup()
        {
            using (Mutex cleanupMutex = new Mutex(false, cleanupMutexName))
            {
                AcquireMutex(cleanupMutex);
                cleanupMutex.ReleaseMutex();
            }
        }

        internal static void CleanupDriverIfLastInstance(NotificationLevel notificationLevel = NotificationLevel.All)
        {
            using (Mutex cleanupMutex = new Mutex(false, cleanupMutexName))
            {
                AcquireMutex(cleanupMutex);

                try
                {
                    if (!IsLastInstance())
                        return;

                    StartDriverCleanup(notificationLevel);
                }
                finally
                {
                    cleanupMutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Waits for the mutex. An abandoned mutex (previous owner exited without
        /// releasing it) is still acquired by the caller, so treat it as success.
        /// </summary>
        private static bool AcquireMutex(Mutex mutex, int millisecondsTimeout = Timeout.Infinite)
        {
            try
            {
                return mutex.WaitOne(millisecondsTimeout);
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
        }

        private static bool IsLastInstance()
        {
            int currentProcessId;
            using (Process currentProcess = Process.GetCurrentProcess())
                currentProcessId = currentProcess.Id;

            foreach (string name in driverUsers)
            {
                Process[] processes = Process.GetProcessesByName(name);

                try
                {
                    foreach (Process process in processes)
                    {
                        try
                        {
                            if (process.Id != currentProcessId && !IsCleanupProcess(process.Id))
                                return false;
                        }
                        catch
                        {
                        }
                    }
                }
                finally
                {
                    foreach (Process process in processes)
                        process.Dispose();
                }
            }

            return true;
        }

        private static bool IsCleanupProcess(int processId)
        {
            Mutex marker;
            if (Mutex.TryOpenExisting(cleanupProcessMarkerPrefix + processId, out marker))
            {
                marker.Dispose();
                return true;
            }

            return false;
        }

        private static bool StartDriverCleanup(NotificationLevel notificationLevel)
        {
            if (IsDriverCleanupMode)
                return false;

            try
            {
                string exePath = Process.GetCurrentProcess().MainModule.FileName;
                string arguments = CleanupArgument + " " + NotificationsArgumentPrefix + notificationLevel.ToString().ToLowerInvariant();

                Process.Start(new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = arguments,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });

                return true;
            }
            catch
            {
                // Cleanup must never prevent the application from closing.
                return false;
            }
        }
    }
}
