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

using System.Collections.Specialized;
using System.Data;
using Yupi.Core.Io;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Core.Settings
{
    /// <summary>
    ///     Class Languages.
    /// </summary>
    internal class ServerLanguageSettings
    {
        /// <summary>
        ///     The texts
        /// </summary>
        internal HybridDictionary Texts;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerLanguageSettings" /> class.
        /// </summary>
        /// <param name="language">The language.</param>
        internal ServerLanguageSettings(string language)
        {
            Texts = new HybridDictionary();

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery($"SELECT * FROM server_langs WHERE lang = '{language}' ORDER BY id DESC");

                DataTable table = commitableQueryReactor.GetTable();

                if (table == null)
                    return;

                foreach (DataRow dataRow in table.Rows)
                {
                    string name = dataRow["name"].ToString();
                    string text = dataRow["text"].ToString();
                    Texts.Add(name, text);
                }
            }
        }

        /// <summary>
        ///     Gets the variable.
        /// </summary>
        /// <param name="var">The variable.</param>
        /// <returns>System.String.</returns>
        internal string GetVar(string var)
        {
            if (Texts.Contains(var))
                return Texts[var].ToString();

            Writer.WriteLine("Variable not found: " + var, "Yupi.Languages");

            return "Language variable not Found: " + var;
        }

        /// <summary>
        ///     Counts this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        internal int Count() => Texts.Count;
    }
}