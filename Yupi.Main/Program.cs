using System;
using System.Threading;
using Yupi.Util;

namespace Yupi.Main
{
    internal class MainClass
    {
        private static Server Yupi;

        public static void Main(string[] args)
        {
            ResetConsole();
            Console.Title = "Yupi!";
            Yupi = new Server();
            Yupi.Run();
            ReadLoop();
        }

        private static void ReadLoop()
        {
            while (true) Thread.Sleep(1000);
        }

        private static void ResetConsole()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            ShowEnvironmentMessage();
        }

        private static void ShowEnvironmentMessage()
        {
            var updateManager = new YupiUpdatesManager();

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
    }
}