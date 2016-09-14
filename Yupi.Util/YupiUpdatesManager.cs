// ---------------------------------------------------------------------------------
// <copyright file="YupiUpdatesManager.cs" company="https://github.com/sant0ro/Yupi">
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