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
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Settings;
using Yupi.Emulator.Data;

namespace Yupi.Emulator
{
     public class Program
    {
     public const uint ScClose = 0xF060;

        /// <summary>
        ///     Main Void of Yupi.Emulator
        /// </summary>
        /// <param name="args">The arguments.</param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        [STAThread]
        public static void Main(string[] args)
        {
            StartProgram();

			#if DEBUG
			// Autostart on debug sessions
			InitEnvironment();
			#endif

            ReadLoop();
        }

     public static void ReadLoop()
        {
            while (Yupi.IsLive || Yupi.IsReady)
            {
                Console.CursorVisible = true;

                ConsoleCommandHandler.InvokeCommand(Console.ReadLine());

                YupiWriterManager.WriteLine("Waiting For Command Input...", "Yupi.Boot", ConsoleColor.DarkCyan);
            }
        }

     public static void StartProgram()
        {
            YupiUpdatesManager.Init();

            YupiUpdatesManager.ShowInitialMessage();

            ShowEnvironmentMessage();

            Yupi.IsReady = true;

            Console.Title = "Yupi! | Waiting for Input.";

            YupiWriterManager.WriteLine("Waiting For Command Input...", "Yupi.Boot", ConsoleColor.DarkCyan);
        }

     public static void ResetConsole()
        {
            Console.Clear();

            ShowEnvironmentMessage();
        }

     public static void StartEverything()
        {
            InitEnvironment();
        }

        public static void ShowEnvironmentMessage()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine(@"                " + @"██╗   ██╗██╗   ██╗██████╗ ██╗██╗");
            Console.WriteLine(@"                " + @"╚██╗ ██╔╝██║   ██║██╔══██╗██║██║");
            Console.WriteLine(@"                " + @" ╚████╔╝ ██║   ██║██████╔╝██║██║");
            Console.WriteLine(@"                " + @"  ╚██╔╝  ██║   ██║██╔═══╝ ██║╚═╝");
            Console.WriteLine(@"                " + @"   ██║   ╚██████╔╝██║     ██║██╗");
            Console.WriteLine(@"                " + @"   ╚═╝    ╚═════╝ ╚═╝     ╚═╝╚═╝");
            Console.WriteLine(@"                " + @"                                ");
            Console.WriteLine();
            Console.WriteLine(@"        " + @"  BUILD " + YupiUpdatesManager.GithubVersionTag + " RELEASE R63 POST SHUFFLE");
            Console.WriteLine(@"        " + @"  .NET Framework " + Environment.Version + " C# 6 Roslyn");
            Console.WriteLine();
        }

        /// <summary>
        ///     Load the Yupi Environment
        /// </summary>
        public static void InitEnvironment()
        {
            if (Yupi.IsLive)
                return;

            Yupi.IsReady = false;

            Console.CursorVisible = false;

            Yupi.Initialize();
        }
    }
}