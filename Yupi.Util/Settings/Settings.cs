namespace Yupi.Util.Settings
{
    using System;
    using System.IO;
    using System.Reflection;

    using Config.Net;

    public static class Settings
    {
        #region Fields

        private static readonly string ConfigDir;

        #endregion Fields

        #region Constructors

        static Settings()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string assemblyDir = Directory.GetParent(assemblyLocation).FullName;

            // TODO Get the directory structure sorted!
            assemblyDir = Directory.GetParent(assemblyDir).Parent.Parent.FullName;

            ConfigDir = Path.Combine(assemblyDir, "Config", "Settings");

            if (!Directory.Exists(ConfigDir))
            {
                Directory.CreateDirectory(ConfigDir);
            }
        }

        #endregion Constructors

        #region Methods

        public static string GetPath(string fileName)
        {
            return Path.Combine(ConfigDir, fileName);
        }

        #endregion Methods
    }
}