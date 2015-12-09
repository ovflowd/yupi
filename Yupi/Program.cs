using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Yupi.Core.Settings;
using Yupi.Data;

namespace Yupi
{
    internal class Program
    {
        /// <summary>
        /// Main Void of Azure.Emulator
        /// </summary>
        /// <param name="args">The arguments.</param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public static void Main(string[] args)
        {
            StartEverything();

            while (Yupi.IsLive)
            {
                Console.CursorVisible = true;
                ConsoleCommandHandler.InvokeCommand(Console.ReadLine());
            }
        }

        private static void StartEverything()
        {
            StartConsoleWindow();
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), ScClose, 0);
            InitEnvironment();
        }

        public static void StartConsoleWindow()
        {
            //Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            //Console.SetWindowSize(Console.LargestWindowWidth > 149 ? 150 : Console.WindowWidth, Console.LargestWindowHeight > 49 ? 50 : Console.WindowHeight);
            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine(@"     " + @"                                    88 88");
            Console.WriteLine(@"     " + "                                    \"\" 88");
            Console.WriteLine(@"     " + @"                                       88");
            Console.WriteLine(@"     " + @"8b       d8 88       88 8b,dPPYba,  88 88");
            Console.WriteLine(@"     " + "`8b     d8' 88       88 88P'    \"8a 88 88 ");
            Console.WriteLine(@"     " + " `8b   d8'  88       88 88       d8 88 \"\"");
            Console.WriteLine(@"     " + "  `8b,d8'   \"8a,   ,a88 88b,   ,a8\" 88 aa");
            Console.WriteLine(@"     " + "    Y88'     `\"YbbdP'Y8 88`YbbdP\"'  88 88");
            Console.WriteLine(@"     " + @"    d8'                 88               ");
            Console.WriteLine(@"     " + @"   d8'                  88               ");
            Console.WriteLine();
            Console.WriteLine(@"     " + @"  BUILD " + Yupi.Version + "." + Yupi.Build + " RELEASE 63B CRYPTO BOTH SIDE");
            Console.WriteLine(@"     " + @"  .NET Framework " + Environment.Version + "     C# 6 Roslyn");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine(Console.LargestWindowWidth > 149 ? "---------------------------------------------------------------------------------------------------------------------------------------------------" : "-------------------------------------------------------------------------");
        }

        /// <summary>
        /// Initialize the Yupi Environment
        /// </summary>
        public static void InitEnvironment()
        {
            if (Yupi.IsLive)
                return;

            Console.CursorVisible = false;
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += ExceptionHandler;
            Yupi.Initialize();
        }

        /// <summary>
        /// Mies the handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            ServerLogManager.DisablePrimaryWriting(true);
            var ex = (Exception)args.ExceptionObject;
            ServerLogManager.LogCriticalException($"SYSTEM CRITICAL EXCEPTION: {ex}");
        }

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, uint nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        internal const uint ScClose = 0xF060;
    }
}