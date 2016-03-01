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

using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Net.Web;

namespace Yupi.Emulator.Data
{
    class YupiUpdatesManager
    {
        /// <summary>
        ///     Update Message
        /// </summary>
        private static DataSet _updateMessage = new DataSet();

        /// <summary>
        ///     Remote Github HEAD Commit Hash
        /// </summary>
        private static string _lastVersion = "";

        /// <summary>
        ///     Running Version Tag
        /// </summary>
        public static readonly string GithubVersionTag = Assembly.GetExecutingAssembly().GetCustomAttributes(false).OfType<AssemblyFileVersionAttribute>().Single().Version;

        /// <summary>
        ///     Running Version
        /// </summary>
        public static readonly string GithubVersion = Assembly.GetExecutingAssembly().GetCustomAttributes(false).OfType<AssemblyInformationalVersionAttribute>().Single().InformationalVersion;

        /// <summary>
        ///     Store Update Message
        /// </summary>
        public static void Init() => _updateMessage = WebManager.HttpGetJsonDataset(Yupi.GithubUpdateFile);

        /// <summary>
        ///     Generate Update Message and Echoes
        /// </summary>
        private static void GenerateUpdateMessage(DataSet message)
        {
            DataTable dataTable = message.Tables["items"];

            foreach (DataRow row in dataTable.Rows)
                YupiWriterManager.WriteLine(row["message"].ToString(), row["title"].ToString(), (ConsoleColor)Enum.Parse(typeof(ConsoleColor), row["color"].ToString()));
        }

        /// <summary>
        ///     Show Initial Message
        /// </summary>
        public static void ShowInitialMessage()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.Clear();

            Console.WriteLine();

            GenerateUpdateMessage(_updateMessage);

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
        }

        /// <summary>
        ///     Show if You're Using Last Version [Not Useful yet]
        /// </summary>
        public static void ShowVersionMessage()
        {
            if (CompareVersion())
                YupiWriterManager.WriteLine("You're running Last Yupi Version from Repo.", "Yupi.Repo");
            else
                YupiWriterManager.WriteLine("You're not Updated with Yupi Repo! Please Download last Version", "Yupi.Repo", ConsoleColor.DarkYellow);
        }

        /// <summary>
        ///     Compare Downloaded Version with Github Version [Not Useful yet]
        /// </summary>
        public static bool CompareVersion() => GithubVersion == _lastVersion || _lastVersion == string.Empty;
    }
}
