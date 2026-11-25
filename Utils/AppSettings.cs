using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace ZenStatesDebugTool
{
    [Serializable]
    public sealed class AppSettings
    {
        public const int VersionMajor = 1;
        public const int VersionMinor = 0;

        private static readonly string Filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");

        private static AppSettings _instance;

        public static AppSettings Instance => _instance ?? (_instance = Load());

        public string Version { get; set; } = new Version(VersionMajor, VersionMinor).ToString();

        public bool AutoUninstallDriver { get; set; } = true;

        public int AutoUninstallDriverNotificationLevel { get; set; } = (int)DriverCleaner.NotificationLevel.All;

        // 0 = follow Windows, 1 = light, 2 = dark (Theming.ThemeMode); light by default, changed by the title bar toggle
        public int ThemeMode { get; set; } = 1;

        // Log panel beside the tabs, toggled from the title bar
        public bool ShowLogPanel { get; set; } = true;

        public const string DefaultStartTab = "tabPagePbo";

        // Name of the tab page selected when the application starts
        public string DefaultTab { get; set; } = DefaultStartTab;

        public const double DefaultMaxCpuVoltage = 1.35;

        // Highest voltage the manual OC voltage (VID) control on the CPU tab offers, in volts
        public double MaxCpuVoltage { get; set; } = DefaultMaxCpuVoltage;

        private static AppSettings Load()
        {
            try
            {
                if (File.Exists(Filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                    using (StreamReader reader = new StreamReader(Filename))
                        return (AppSettings)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new AppSettings();
        }

        public void Save()
        {
            try
            {
                Version = new Version(VersionMajor, VersionMinor).ToString();

                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                using (StreamWriter writer = new StreamWriter(Filename, false))
                    serializer.Serialize(writer, this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
