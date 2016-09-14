// ---------------------------------------------------------------------------------
// <copyright file="Program.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Main
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using Yupi.Util;

    class MainClass
    {
        #region Fields

        private static Server Yupi;

        #endregion Fields

        #region Methods

        public static void Main(string[] args)
        {
            ResetConsole();
            Console.Title = "Yupi!";
            Yupi = new Server();
            Yupi.Run();
            ReadLoop();
        }

        static void ReadLoop()
        {
            while (true)
            {
                // TODO Reimplement
                Thread.Sleep(1000);
                //ConsoleCommandHandler.InvokeCommand ();

                //YupiWriterManager.WriteLine ("Waiting For Command Input...", "Yupi.Boot", ConsoleColor.DarkCyan);
            }
        }

        static void ResetConsole()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            ShowEnvironmentMessage();
        }

        static void ShowEnvironmentMessage()
        {
            YupiUpdatesManager updateManager = new YupiUpdatesManager();

            Console.WriteLine();
            Console.WriteLine("                " + "██╗   ██╗██╗   ██╗██████╗ ██╗██╗");
            Console.WriteLine("                " + "╚██╗ ██╔╝██║   ██║██╔══██╗██║██║");
            Console.WriteLine("                " + " ╚████╔╝ ██║   ██║██████╔╝██║██║");
            Console.WriteLine("                " + "  ╚██╔╝  ██║   ██║██╔═══╝ ██║╚═╝");
            Console.WriteLine("                " + "   ██║   ╚██████╔╝██║     ██║██╗");
            Console.WriteLine("                " + "   ╚═╝    ╚═════╝ ╚═╝     ╚═╝╚═╝");
            Console.WriteLine("                " + "                                ");
            Console.WriteLine();
            Console.WriteLine("        " + "  BUILD " + updateManager.GithubVersionTag + " RELEASE R63 POST SHUFFLE");
            Console.WriteLine("        " + "  .NET Framework " + Environment.Version + " C# 6 Roslyn");
            Console.WriteLine();
        }

        #endregion Methods
    }
}