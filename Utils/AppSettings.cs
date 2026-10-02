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
