using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Yupi.Core.Io;
using Yupi.Core.Security.BlackWords.Enums;
using Yupi.Core.Security.BlackWords.Structs;

namespace Yupi.Core.Security.BlackWords
{
    /// <summary>
    /// Class BlackWordsManager.
    /// </summary>
    internal static class BlackWordsManager
    {
        /// <summary>
        /// The words
        /// </summary>
        private static readonly List<BlackWord> Words = new List<BlackWord>();

        /// <summary>
        /// The replaces
        /// </summary>
        private static readonly Dictionary<BlackWordType, BlackWordTypeSettings> Replaces = new Dictionary<BlackWordType, BlackWordTypeSettings>();

        private static readonly BlackWord Empty = new BlackWord(string.Empty, BlackWordType.All);

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public static void Load()
        {
            using (var adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery("SELECT * FROM server_blackwords");
                var table = adapter.GetTable();

                if (table == null)
                    return;

                foreach (DataRow row in table.Rows)
                {
                    var word = row["word"].ToString();
                    var typeStr = row["type"].ToString();

                    AddPrivateBlackWord(typeStr, word);
                }
            }

            Writer.WriteLine("Loaded " + Words.Count + " BlackWords", "Yupi.Security");
            //Console.WriteLine();
        }

        /// <summary>
        /// Reloads this instance.
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

            using (var adapter = Yupi.GetDatabaseManager().GetQueryReactor())
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

            var wordStruct = Words.FirstOrDefault(wordS => wordS.Type == type && word.Contains(wordS.Word));

            if (string.IsNullOrEmpty(wordStruct.Word))
                return;

            Words.Remove(wordStruct);

            using (var adapter = Yupi.GetDatabaseManager().GetQueryReactor())
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
                    Writer.WriteLine("Word type [all] it's reserved for system. Word: " + word, "Yupi.Security", ConsoleColor.DarkRed);
                    return;

                default:
                    Writer.WriteLine("Undefined type [" + typeStr + "] of word: " + word, "Yupi.Security", ConsoleColor.DarkRed);
                    return;
            }

            Words.Add(new BlackWord(word, type));

            if (Replaces.ContainsKey(type))
                return;

            string filter = UserChatInputFilter.Default, alert = "User [{0}] with Id: {1} has said a blackword. Word: {2}. Type: {3}. Message: {4}", imageAlert = "bobba";

            var maxAdvices = 7u;
            bool autoBan = true, showMessage = true;

            if (File.Exists("Settings\\BlackWords\\" + typeStr + ".ini"))
            {
                foreach (var array in File.ReadAllLines("Settings\\BlackWords\\" + typeStr + ".ini").Where(line => !line.StartsWith("#") || !line.StartsWith("//") || line.Contains("=")).Select(line => line.Split('=')))
                {
                    if (array[0] == "filterType") filter = array[1];
                    if (array[0] == "maxAdvices") maxAdvices = uint.Parse(array[1]);
                    if (array[0] == "alertImage") imageAlert = array[1];
                    if (array[0] == "autoBan") autoBan = array[1] == "true";
                    if (array[0] == "showMessage") showMessage = array[1] == "true";
                }
            }

            if (File.Exists("Settings\\BlackWords\\" + typeStr + ".alert.txt"))
                alert = File.ReadAllText("Settings\\BlackWords\\" + typeStr + ".alert.txt");

            Replaces.Add(type, new BlackWordTypeSettings(filter, alert, maxAdvices, imageAlert, autoBan, showMessage));
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>BlackWordTypeSettings.</returns>
        public static BlackWordTypeSettings GetSettings(BlackWordType type) => Replaces[type];

        /// <summary>
        /// Checks the specified string.
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

            var data = Replaces[type];

            str = UserChatInputFilter.Replace(data.Filter, str);

            var wordFirst = Words.FirstOrDefault(wordStruct => wordStruct.Type == type && str.Contains(wordStruct.Word));

            word = wordFirst;

            return !string.IsNullOrEmpty(wordFirst.Word);
        }
    }
}