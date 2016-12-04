// ---------------------------------------------------------------------------------
// <copyright file="DatabaseSettings.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Util.Settings
{
    using System;

    using Config.Net;

    public class DatabaseSettings : SettingsContainer
    {
        private static readonly DatabaseSettings instance = new DatabaseSettings();

        public static DatabaseSettings Instance
        {
            get 
            {
                return instance; 
            }
        }

        #region Fields

        public readonly Option<string> Host = new Option<string> ("DB.Host", "localhost");
        public readonly Option<string> Name = new Option<string> ("DB.Name", "yupi");
        public readonly Option<string> Password = new Option<string> ("DB.Password", null);
        public readonly Option<DatabaseType> Type = new Option<DatabaseType> ("DB.Type", DatabaseType.SQLite);
        public readonly Option<string> Username = new Option<string> ("DB.Username", "yupi");

        #endregion Fields

        #region Methods

        protected override void OnConfigure(IConfigConfiguration configuration)
        {
            configuration.UseIniFile(Settings.GetPath("database.ini"));
        }

        public string BuildConnectionString()
        {
            switch ((DatabaseType)Type) {
            case DatabaseType.MySQL:
                return "Server = " + Host + "; Database = " + Name + "; Uid = " + Username + ";Pwd = " + Password;
            case DatabaseType.SQLite:
                return "Data Source="+Name+".sqlite;Version=3;";
            default:
                throw new InvalidOperationException ("Unsupported type");
            }
        }

        #endregion Methods
    }
}