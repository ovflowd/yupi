using System.Collections.Generic;
using System.Data;
using Yupi.Data.Base.Sessions.Interfaces;

namespace Yupi.Core.Settings
{
    /// <summary>
    /// Class ServerDatabaseSettings.
    /// </summary>
    internal class ServerDatabaseSettings
    {
        /// <summary>
        /// The database data
        /// </summary>
        internal Dictionary<string, string> DbData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerDatabaseSettings"/> class.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal ServerDatabaseSettings(IRegularQueryAdapter dbClient)
        {
            DbData = new Dictionary<string, string>();

            DbData.Clear();
            dbClient.SetQuery("SELECT * FROM server_settings");

            var table = dbClient.GetTable();

            foreach (DataRow dataRow in table.Rows)
                DbData.Add(dataRow[0].ToString(), dataRow[1].ToString());
        }
    }
}