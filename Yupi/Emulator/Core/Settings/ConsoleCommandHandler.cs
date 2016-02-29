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
using System.Diagnostics;
using System.Runtime;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Security;
using Yupi.Emulator.Core.Security.BlackWords;
using Yupi.Emulator.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Core.Settings
{
    /// <summary>
    ///     Class ConsoleCommandHandling.
    /// </summary>
    internal class ConsoleCommandHandler
    {
        /// <summary>
        ///     Gets the game.
        /// </summary>
        /// <returns>HabboHotel.</returns>
        internal static Game.HabboHotel GetGame() => Yupi.GetGame();

        /// <summary>
        ///     Invokes the command.
        /// </summary>
        /// <param name="inputData">The input data.</param>
        internal static void InvokeCommand(string inputData)
        {
            try
            {
                if (string.IsNullOrEmpty(inputData) || YupiWriterManager.DisabledState)
                    return;

                Console.SetCursorPosition(0, Console.CursorTop - 2);

                string firstArgument = inputData, secondArgument = string.Empty;

                if (inputData.Contains(" "))
                {
                    string[] strArguments = inputData.Split(' ');

                    firstArgument = strArguments[0];
                    secondArgument = strArguments[1];
                }

                EraseLine();

                switch (firstArgument)
                {
                    case "clear":
                    case "help":
                    case "start":
                    case "exit":
                        break;
                    default:
                        if (!NotInitialized())
                            return;
                        break;
                }

                switch (firstArgument)
                {
                    case "start":

                        YupiWriterManager.WriteLine("Issued Server Start.", "Yupi.Boot");

                        Program.StartEverything();

                        break;

                    case "exit":

                        if (Yupi.IsLive)
                        {
                            YupiWriterManager.WriteLine("Server Is Running. Issuing Shutdown.", "Yupi.Boot", ConsoleColor.DarkCyan);

                            YupiLogManager.LogMessage($"Server Shutdowning at {DateTime.Now}.");

                            YupiWriterManager.DisablePrimaryWriting(true);

                            YupiWriterManager.WriteLine("Shutdown Initalized", "Yupi.Life", ConsoleColor.DarkYellow);

                            Yupi.PerformShutDown();
                        }

                        YupiWriterManager.WriteLine("Exiting. Yupi Environment", "Yupi.Boot");

                        Environment.Exit(0);

                        break;

                    case "shutdown":

                        YupiLogManager.LogMessage($"Server Shutdowning at {DateTime.Now}.");

                        YupiWriterManager.DisablePrimaryWriting(true);

                        YupiWriterManager.WriteLine("Shutdown Initalized", "Yupi.Life", ConsoleColor.DarkYellow);

                        Yupi.PerformShutDown();

                        YupiWriterManager.DisablePrimaryWriting(false);

                        YupiWriterManager.WriteLine("Waiting For Commands.", "Yupi.Boot");

                        break;

                    case "restart":
                        YupiLogManager.LogMessage($"Server Restarting at {DateTime.Now}.");

                        YupiWriterManager.DisablePrimaryWriting(true);

                        YupiWriterManager.WriteLine("Restart Initialized", "Yupi.Life", ConsoleColor.DarkYellow);

                        Yupi.PerformRestart();

                        YupiWriterManager.DisablePrimaryWriting(false);

                        YupiWriterManager.WriteLine("Waiting For Commands.", "Yupi.Boot");

                        break;

                    case "reload":
                        switch (secondArgument)
                        {
                            case "database":
                                Console.WriteLine("Database destroyed");
                                Console.WriteLine();
                                break;

                            case "packets":
                                PacketLibraryManager.ReloadDictionarys();
                                Console.WriteLine("> Packets Reloaded Suceffuly...");
                                Console.WriteLine();
                                break;

                            case "catalogue":
                                FurnitureDataManager.SetCache();

                                using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
                                    GetGame().GetCatalogManager().Init(adapter);

                                FurnitureDataManager.Clear();

                                GetGame()
                                    .GetClientManager()
                                    .QueueBroadcaseMessage(
                                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("PublishShopMessageComposer")));
                                Console.WriteLine("Catalogue was re-loaded.");
                                Console.WriteLine();
                                break;

                            case "modeldata":
                                using (IQueryAdapter adapter2 = Yupi.GetDatabaseManager().GetQueryReactor())
                                    GetGame().GetRoomManager().LoadModels(adapter2);

                                Console.WriteLine("Room models were re-loaded.");
                                Console.WriteLine();
                                break;

                            case "bans":
                                using (IQueryAdapter adapter3 = Yupi.GetDatabaseManager().GetQueryReactor())
                                    GetGame().GetBanManager().LoadBans(adapter3);

                                Console.WriteLine("Bans were re-loaded");
                                Console.WriteLine();
                                break;

                            case "filter":
                                UserChatInputFilter.Reload();
                                BlackWordsManager.Reload();
                                break;
                        }
                        break;

                    case "clear":
                        Program.ShowEnvironmentMessage();

                        break;

                    case "status":
                        TimeSpan uptime = DateTime.Now - Yupi.YupiServerStartDateTime;

                        Console.WriteLine("Server status:");
                        Console.WriteLine();
                        Console.WriteLine("Uptime:");
                        Console.WriteLine("\tDays:    {0}", uptime.Days);
                        Console.WriteLine("\tHours:   {0}", uptime.Hours);
                        Console.WriteLine("\tMinutes: {0}", uptime.Minutes);
                        Console.WriteLine();
                        Console.WriteLine("Stats:");
                        Console.WriteLine("\tAccepted Connections: {0}", GetGame().GetClientManager().ClientCount());
                        Console.WriteLine("\tActive Threads: {0}", Process.GetCurrentProcess().Threads.Count);
                        Console.WriteLine();
                        Console.WriteLine();
                        break;

                    case "gcinfo":
                    {
                        Console.WriteLine("Mode: " + GCSettings.LatencyMode);
                        Console.WriteLine("Is server GC: " + GCSettings.IsServerGC);

                        break;
                    }

                    case "memstat":
                    {
                        Console.WriteLine("GC status:");
                        Console.WriteLine("\tGeneration supported: " + GC.MaxGeneration);
                        Console.WriteLine("\tLatency mode: " + GCSettings.LatencyMode);
                        Console.WriteLine("\tIs server GC: " + GCSettings.IsServerGC);
                        Console.WriteLine();
                        break;
                    }

                    case "memory":
                    {
                        GC.Collect();
                        Console.WriteLine("Memory flushed");

                        break;
                    }

                    case "help":

                        YupiWriterManager.WriteLine("Available Commands: \n", "Yupi.Comm", ConsoleColor.DarkCyan);

                        Console.WriteLine("\tstart");
                        Console.WriteLine("\texit");
                        Console.WriteLine("\tshutdown");
                        Console.WriteLine("\tclear");
                        Console.WriteLine("\tmemory");
                        Console.WriteLine("\tstatus");
                        Console.WriteLine("\trestart");
                        Console.WriteLine("\tmemstat");
                        Console.WriteLine("\treload [catalogue|modeldata|bans|packets|filter|database]");
                        Console.WriteLine();
                        break;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///     Unknowns the command.
        /// </summary>
        private static void EraseLine()
        {
            Console.WriteLine("\r");
        }

        private static bool NotInitialized()
        {
            if (!Yupi.IsLive)
                YupiWriterManager.WriteLine("Server isn't Initialized.", "Yupi.Boot", ConsoleColor.DarkYellow);

            if (!Yupi.IsLive)
                return false;

            return true;
        }
    }
}