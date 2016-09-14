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
using System.Linq;
using System.Reflection;

namespace Yupi.Util
{
    public class YupiUpdatesManager
    {
        /// <summary>
        ///     Remote Github HEAD Commit Hash
        /// </summary>
        private string _lastVersion = "";

        /// <summary>
        ///     Running Version Tag
        /// </summary>
        public readonly string GithubVersionTag =
            Assembly.GetEntryAssembly()
                .GetCustomAttributes(false)
                .OfType<AssemblyFileVersionAttribute>()
                .Single()
                .Version;

        /// <summary>
        ///     Running Version
        /// </summary>
        public readonly string GithubVersion =
            Assembly.GetEntryAssembly()
                .GetCustomAttributes(false)
                .OfType<AssemblyInformationalVersionAttribute>()
                .Single()
                .InformationalVersion;

        /// <summary>
        ///     Store Update Message
        /// </summary>
        public YupiUpdatesManager()
        {
            // TODO Reimplement
            //_updateMessage = WebManager.HttpGetJsonDataset (Yupi.GithubUpdateFile);
        }

        /// <summary>
        ///     Generate Update Message and Echoes
        /// </summary>
        private void GenerateUpdateMessage()
        {
            /*
            if (message == null) {
                return;
            }

            DataTable dataTable = message.Tables["items"];

            foreach (DataRow row in dataTable.Rows)
                YupiWriterManager.WriteLine(row["message"].ToString(), row["title"].ToString(), (ConsoleColor)Enum.Parse(typeof(ConsoleColor), row["color"].ToString()));
                */
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Show Initial Message
        /// </summary>
        public void ShowInitialMessage()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.Clear();

            Console.WriteLine();
            throw new NotImplementedException();
            // GenerateUpdateMessage(_updateMessage);

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
        }

        /// <summary>
        ///     Show if You're Using Last Version [Not Useful yet]
        /// </summary>
        public void ShowVersionMessage()
        {
            /*
			if (CompareVersion())
                YupiWriterManager.WriteLine("You're running Last Yupi Version from Repo.", "Yupi.Repo");
            else
                YupiWriterManager.WriteLine("You're not Updated with Yupi Repo! Please Download last Version", "Yupi.Repo", ConsoleColor.DarkYellow);
                */
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Compare Downloaded Version with Github Version [Not Useful yet]
        /// </summary>
        public bool CompareVersion() => GithubVersion == _lastVersion || _lastVersion == string.Empty;
    }
}