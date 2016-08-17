using System;
using Yupi.Util;
using Yupi.Emulator.Core.Io.Logger;
using System.Reflection;
using System.Linq;
using System.Threading;

namespace Yupi.Main
{
	class MainClass
	{
		private static Server Yupi;

		public static void Main (string[] args)
		{
			ResetConsole();
			Console.Title = "Yupi!";
			Yupi = new Server ();
			Yupi.Run ();
			ReadLoop ();
		}

		static void ReadLoop()
		{
			while (true) {
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
			YupiUpdatesManager updateManager = new YupiUpdatesManager ();

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
