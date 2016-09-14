namespace Yupi.Util.Settings
{
    using System;

    using Config.Net;

    public static class DatabaseSettings
    {
        #region Fields

        public static readonly Setting<string> Host = new Setting<string>("DB.Host", "localhost");
        public static readonly Setting<string> Name = new Setting<string>("DB.Name", "yupi");
        public static readonly Setting<string> Password = new Setting<string>("DB.Password", null);
        public static readonly Setting<DatabaseType> Type = new Setting<DatabaseType>("DB.Type", DatabaseType.SQLite);
        public static readonly Setting<string> Username = new Setting<string>("DB.Username", "yupi");

        #endregion Fields

        #region Constructors

        static DatabaseSettings()
        {
            Cfg.Configuration.UseIniFile(Settings.GetPath("database.ini"));
            Cfg.Write(Type, DatabaseType.MySQL);
        }

        #endregion Constructors
    }
}