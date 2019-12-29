using System;
using System.Windows.Forms;

namespace ZenStatesDebugTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += ApplicationThreadException;
            try
            {
                Form MainForm = new SettingsForm();
                MainForm.Text = Application.ProductName + " " + Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf("."));
                Application.Run(MainForm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Application.Exit();
            }
        }

        static void ApplicationThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // Handle your exception here...
            MessageBox.Show(e.Exception.Message, "Error");
        }
    }
}
