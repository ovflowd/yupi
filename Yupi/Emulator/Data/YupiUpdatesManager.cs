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

        public static void Init()
        {
            try
            {
                _updateMessage = WebManager.HttpGetJsonDataset(Yupi.GithubUpdateFile);
            }
            catch
            {
                //YupiWriterManager.WriteLine("Failed Retrieving Yupi Repository Details.", "Yupi.Boot", ConsoleColor.DarkYellow);
            }

            try
            {
                _lastVersion = WebManager.HttpGetJsonObject(Yupi.GithubCommitApi).sha.ToString();
            }
            catch
            {
                //YupiWriterManager.WriteLine("Failed Retrieving Last Yupi Commit Details.", "Yupi.Boot", ConsoleColor.DarkYellow);
            }
        }

        private static void GenerateUpdateMessage(DataSet message)
        {
            DataTable dataTable = message.Tables["items"];

            foreach (DataRow row in dataTable.Rows)
                YupiWriterManager.WriteLine(row["message"].ToString(), row["title"].ToString(), (ConsoleColor)Enum.Parse(typeof(ConsoleColor), row["color"].ToString()));
        }

        public static void ShowInitialMessage()
        {
            string message = " Message from Servers ";

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(" " + new String('=', Console.WindowWidth / 2 - message.Length - 1) + message + new String('=', Console.WindowWidth / 2 - 1) + " ");

            GenerateUpdateMessage(_updateMessage);

            ShowVersionMessage();

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();

            Console.WriteLine(" " + new String('=', Console.WindowWidth - 2) + " ");
        }

        public static void ShowVersionMessage()
        {
            if (CompareVersion())
                YupiWriterManager.WriteLine("You're running Last Yupi Version from Repo.", "Yupi.Repo");
            else
                YupiWriterManager.WriteLine("You're not Updated with Yupi Repo! Please Download last Version", "Yupi.Repo", ConsoleColor.DarkYellow);
        }

        public static bool CompareVersion() => GithubVersion == _lastVersion || _lastVersion == string.Empty;

        public static string GetRemoteVersion() => _lastVersion;
    }
}
