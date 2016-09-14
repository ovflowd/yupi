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