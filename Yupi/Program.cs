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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Yupi.Core.Settings;

namespace Yupi
{   
    internal class Program
    {
        internal const uint ScClose = 0xF060;

        /// <summary>
        ///     Main Void of Yupi.Emulator
        /// </summary>
        /// <param name="args">The arguments.</param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]    
        [STAThread]
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
            SetConsoleLayout();

            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), ScClose, 0);

            InitEnvironment();
        }

        public static void SetConsoleLayout()
        {
            Console.Clear();
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
            Console.WriteLine(@"     " + @"  BUILD " + Yupi.ServerVersion + "." + Yupi.ServerBuild + " RELEASE 63B CRYPTO BOTH SIDE");
            Console.WriteLine(@"     " + @"  .NET Framework " + Environment.Version + "     C# 6 Roslyn");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Load the Yupi Environment
        /// </summary>
        public static void InitEnvironment()
        {
            if (Yupi.IsLive)
                return;

            Console.CursorVisible = false;

            Yupi.Initialize();
        }

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, uint nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
    }
}