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

namespace Yupi.Game.Pets
{
    /// <summary>
    ///     Class PetLocale.
    /// </summary>
    internal class PetLocale
    {
        /// <summary>
        ///     The _values
        /// </summary>
        private static Dictionary<string, string[]> _values;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal static void Init(IQueryAdapter dbClient)
        {
            _values = new Dictionary<string, string[]>();

            dbClient.SetQuery("SELECT * FROM pets_speech");
            DataTable table = dbClient.GetTable();

            foreach (DataRow dataRow in table.Rows)
                _values.Add(dataRow["pet_id"].ToString(), dataRow["responses"].ToString().Split(';'));
        }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String[].</returns>
        internal static string[] GetValue(string key)
        {
            string[] result;

            return _values.TryGetValue(key, out result) ? result : new[] {key};
        }
    }
}