/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System.Collections.Generic;
using System.Data;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Core.Settings
{
    /// <summary>
    ///     Class ServerDatabaseSettings.
    /// </summary>
    internal class ServerDatabaseSettings
    {
        /// <summary>
        ///     The database data
        /// </summary>
        internal Dictionary<string, string> DbData;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerDatabaseSettings" /> class.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal ServerDatabaseSettings(IQueryAdapter dbClient)
        {
            DbData = new Dictionary<string, string>();

            DbData.Clear();
            dbClient.SetQuery("SELECT * FROM server_settings");

            DataTable table = dbClient.GetTable();

            foreach (DataRow dataRow in table.Rows)
                DbData.Add(dataRow[0].ToString(), dataRow[1].ToString());
        }
    }
}