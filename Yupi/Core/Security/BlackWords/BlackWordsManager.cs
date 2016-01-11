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
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Yupi.Core.Io;
using Yupi.Core.Security.BlackWords.Enums;
using Yupi.Core.Security.BlackWords.Structs;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Core.Security.BlackWords
{
    /// <summary>
    ///     Class BlackWordsManager.
    /// </summary>
    internal static class BlackWordsManager
    {
        /// <summary>
        ///     The words
        /// </summary>
        private static readonly List<BlackWord> Words = new List<BlackWord>();

        /// <summary>
        ///     The replaces
        /// </summary>
        private static readonly Dictionary<BlackWordType, BlackWordTypeSettings> Replaces =
            new Dictionary<BlackWordType, BlackWordTypeSettings>();

        private static readonly BlackWord Empty = new BlackWord(string.Empty, BlackWordType.All);

        /// <summary>
        ///     Loads this instance.
        /// </summary>
        public static void Load()
        {
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery("SELECT * FROM server_blackwords");
                DataTable table = adapter.GetTable();

                if (table == null)
                    return;

                foreach (DataRow row in table.Rows)
                {
                    string word = row["word"].ToString();
                    string typeStr = row["type"].ToString();

                    AddPrivateBlackWord(typeStr, word);
                }
            }

            Writer.WriteLine("Loaded " + Words.Count + " BlackWords", "Yupi.Security");
            //Console.WriteLine();
        }

        /// <summary>
        ///     Reloads this instance.
        /// </summary>
        public static void Reload()
        {
            Words.Clear();
            Replaces.Clear();

            Load();
        }

        public static void AddBlackWord(string typeStr, string word)
        {
            BlackWordType type;

            if (!Enum.TryParse(typeStr, true, out type))
                return;

            if (Words.Any(wordStruct => wordStruct.Type == type && word.Contains(wordStruct.Word)))
                return;

            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery("INSERT INTO server_blackwords VALUES (null, @word, @type)");
                adapter.AddParameter("word", word);
                adapter.AddParameter("type", typeStr);
                adapter.RunQuery();
            }

            AddPrivateBlackWord(typeStr, word);
        }

        public static void DeleteBlackWord(string typeStr, string word)
        {
            BlackWordType type;

            if (!Enum.TryParse(typeStr, true, out type))
                return;

            BlackWord wordStruct = Words.FirstOrDefault(wordS => wordS.Type == type && word.Contains(wordS.Word));

            if (string.IsNullOrEmpty(wordStruct.Word))
                return;

            Words.Remove(wordStruct);

            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery("DELETE FROM server_blackwords WHERE word = @word AND type = @type");
                adapter.AddParameter("word", word);
                adapter.AddParameter("type", typeStr);
                adapter.RunQuery();
            }
        }

        private static void AddPrivateBlackWord(string typeStr, string word)
        {
            BlackWordType type;

            switch (typeStr)
            {
                case "hotel":
                    type = BlackWordType.Hotel;
                    break;

                case "insult":
                    type = BlackWordType.Insult;
                    break;

                case "all":
                    Writer.WriteLine("Word type [all] it's reserved for system. Word: " + word, "Yupi.Security",
                        ConsoleColor.DarkRed);
                    return;

                default:
                    Writer.WriteLine("Undefined type [" + typeStr + "] of word: " + word, "Yupi.Security",
                        ConsoleColor.DarkRed);
                    return;
            }

            Words.Add(new BlackWord(word, type));

            if (Replaces.ContainsKey(type))
                return;

            string filter = UserChatInputFilter.Default,
                alert = "User [{0}] with Id: {1} has said a blackword. Word: {2}. Type: {3}. Message: {4}",
                imageAlert = "bobba";

            uint maxAdvices = 7u;
            bool autoBan = true, showMessage = true;

            if (File.Exists($"{Yupi.YupiVariablesDirectory}\\Settings\\BlackWords\\" + typeStr + ".ini"))
            {
                foreach (
                    string[] array in
                        File.ReadAllLines($"{Yupi.YupiVariablesDirectory}\\Settings\\BlackWords\\" + typeStr + ".ini")
                            .Where(line => !line.StartsWith("#") || !line.StartsWith("//") || line.Contains("="))
                            .Select(line => line.Split('=')))
                {
                    if (array[0] == "filterType") filter = array[1];
                    if (array[0] == "maxAdvices") maxAdvices = uint.Parse(array[1]);
                    if (array[0] == "alertImage") imageAlert = array[1];
                    if (array[0] == "autoBan") autoBan = array[1] == "true";
                    if (array[0] == "showMessage") showMessage = array[1] == "true";
                }
            }

            if (File.Exists($"{Yupi.YupiVariablesDirectory}\\Settings\\BlackWords\\" + typeStr + ".alert.txt"))
                alert =
                    File.ReadAllText($"{Yupi.YupiVariablesDirectory}\\Settings\\BlackWords\\" + typeStr + ".alert.txt");

            Replaces.Add(type, new BlackWordTypeSettings(filter, alert, maxAdvices, imageAlert, autoBan, showMessage));
        }

        /// <summary>
        ///     Gets the settings.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>BlackWordTypeSettings.</returns>
        public static BlackWordTypeSettings GetSettings(BlackWordType type) => Replaces[type];

        /// <summary>
        ///     Checks the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="type">The type.</param>
        /// <param name="word">The word.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Check(string str, BlackWordType type, out BlackWord word)
        {
            word = Empty;

            if (!Replaces.ContainsKey(type))
                return false;

            BlackWordTypeSettings data = Replaces[type];

            str = UserChatInputFilter.Replace(data.Filter, str);

            BlackWord wordFirst = Words.FirstOrDefault(wordStruct => wordStruct.Type == type && str.Contains(wordStruct.Word));

            word = wordFirst;

            return !string.IsNullOrEmpty(wordFirst.Word);
        }
    }
}