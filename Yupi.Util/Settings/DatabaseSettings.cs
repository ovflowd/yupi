using System;
using Config.Net;

namespace Yupi.Util.Settings
{
    public static class DatabaseSettings
    {
        public static readonly Setting<DatabaseType> Type = new Setting<DatabaseType>("DB.Type", DatabaseType.SQLite);

        public static readonly Setting<string> Name = new Setting<string>("DB.Name", "yupi");
        public static readonly Setting<string> Username = new Setting<string>("DB.Username", "yupi");
        public static readonly Setting<string> Password = new Setting<string>("DB.Password", null);
        public static readonly Setting<string> Host = new Setting<string>("DB.Host", "localhost");

        static DatabaseSettings()
        {
            Cfg.Configuration.UseIniFile(Settings.GetPath("database.ini"));
            Cfg.Write(Type, DatabaseType.MySQL);
        }
    }
}