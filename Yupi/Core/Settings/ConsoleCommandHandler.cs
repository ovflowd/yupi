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
using Yupi.Core.Io;
using Yupi.Core.Security;
using Yupi.Core.Security.BlackWords;
using Yupi.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Core.Settings
{
    /// <summary>
    ///     Class ConsoleCommandHandling.
    /// </summary>
    internal class ConsoleCommandHandler
    {
        /// <summary>
        ///     Gets the game.
        /// </summary>
        /// <returns>Game.</returns>
        internal static Game.Game GetGame() => Yupi.GetGame();

        /// <summary>
        ///     Invokes the command.
        /// </summary>
        /// <param name="inputData">The input data.</param>
        internal static void InvokeCommand(string inputData)
        {
            if (string.IsNullOrEmpty(inputData) && ServerLogManager.DisabledState)
                return;

            try
            {
                if (inputData == null)
                    return;

                string firstArgument = inputData, secondArgument = string.Empty;

                if (inputData.Contains(" "))
                {
                    string[] strArguments = inputData.Split(' ');

                    firstArgument = strArguments[0];
                    secondArgument = strArguments[1];
                }

                switch (firstArgument)
                {
                    case "shutdown":
                        ServerLogManager.LogMessage($"Server Shutdowning at {DateTime.Now}");

                        ServerLogManager.DisablePrimaryWriting(true);

                        Writer.WriteLine("Shutdown Initalized", "Yupi.Life", ConsoleColor.DarkYellow);

                        Yupi.PerformShutDown();

                        Console.WriteLine();
                        break;

                    case "restart":
                        ServerLogManager.LogMessage($"Server Restarting at {DateTime.Now}");

                        ServerLogManager.DisablePrimaryWriting(true);

                        Writer.WriteLine("Restart Initialized", "Yupi.Life", ConsoleColor.DarkYellow);

                        Yupi.PerformRestart();
                        Console.WriteLine();
                        break;

                    case "reload":
                        switch (secondArgument)
                        {
                            case "database":
                                Console.WriteLine("Database destroyed");
                                Console.WriteLine();
                                break;

                            case "packets":
                                LibraryParser.ReloadDictionarys();
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
                                        new ServerMessage(LibraryParser.OutgoingRequest("PublishShopMessageComposer")));
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

                            default:
                                UnknownCommand(inputData);
                                Console.WriteLine();
                                break;
                        }
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    case "status":
                        TimeSpan uptime = DateTime.Now - Yupi.ServerStarted;

                        Console.WriteLine("Server status:");
                        Console.WriteLine();
                        Console.WriteLine("Uptime:");
                        Console.WriteLine("\tDays:    {0}", uptime.Days);
                        Console.WriteLine("\tHours:   {0}", uptime.Hours);
                        Console.WriteLine("\tMinutes: {0}", uptime.Minutes);
                        Console.WriteLine();
                        Console.WriteLine("Stats:");
                        Console.WriteLine("\tAccepted Connections: {0}",
                            Yupi.GetConnectionManager().Manager.AcceptedConnections);
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
                        Console.WriteLine("shutdown");
                        Console.WriteLine("clear");
                        Console.WriteLine("memory");
                        Console.WriteLine("status");
                        Console.WriteLine("restart");
                        Console.WriteLine("memstat");
                        Console.WriteLine("flush catalog");
                        Console.WriteLine("flush modeldata");
                        Console.WriteLine("flush bans");
                        Console.WriteLine("flush packets");
                        Console.WriteLine("flush filter");
                        Console.WriteLine("flush packets");
                        Console.WriteLine("flush database");
                        Console.WriteLine();
                        break;

                    default:
                        UnknownCommand(inputData);
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
        /// <param name="command">The command.</param>
        private static void UnknownCommand(string command)
        {
            Writer.WriteLine("Undefined Command: " + command, "Yupi.Commands");
        }
    }
}