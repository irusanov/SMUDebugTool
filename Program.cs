using System;
using System.Windows.Forms;
using ZenStatesDebugTool.Theming;

namespace ZenStatesDebugTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (Array.Exists(args, DriverCleanup.IsCleanupArgument))
            {
                DriverCleanup.RunCleanupProcess(args);
                return;
            }

            if (Array.Exists(args, ProfileNotification.IsNotifyArgument))
            {
                ProfileNotification.RunNotifyProcess(args);
                return;
            }

            DriverCleanup.WaitForDriverCleanup();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Theme.Initialize((ThemeMode)AppSettings.Instance.ThemeMode);
            Application.ThreadException += ApplicationThreadException;

            // Not for runs started by the startup task (no window) or without a desktop
            bool splash = Environment.UserInteractive && !Array.Exists(args, arg => string.Equals(arg, "--task", StringComparison.OrdinalIgnoreCase));
            if (splash)
                SplashScreen.Show();

            try
            {
                Form MainForm = new SettingsForm();
                MainForm.Shown += (_, __) =>
                {
                    SplashScreen.Close();
                    MainForm.Activate();
                };
                string appString = $"{Application.ProductName} {Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf('.'))}";
#if DEBUG
                appString += " (debug)";
#endif
                MainForm.Text = appString;
                Application.Run(MainForm);
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
                Application.Exit();
            }
            finally
            {
                SplashScreen.Close();
            }
        }

        static void ApplicationThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // Handle your exception here...
            ThemedMessageBox.Show(e.Exception.Message, Properties.Resources.Error);
        }
    }
}
