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
using System.Reflection;
using Yupi.Core.Io;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Data
{
    /// <summary>
    ///     Class ServerCpuLowPriorityWorker.
    /// </summary>
    internal class ServerCpuLowPriorityWorker
    {
        /// <summary>
        ///     The _user peak
        /// </summary>
        private static int _userPeak;

        private static bool _isExecuted;
        private static Stopwatch _lowPriorityStopWatch;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal static void Init(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT userpeak FROM server_status");

            _userPeak = dbClient.GetInteger();

            _lowPriorityStopWatch = new Stopwatch();
            _lowPriorityStopWatch.Start();
        }

        /// <summary>
        ///     Processes the specified caller.
        /// </summary>
        internal static void Process()
        {
            if (_lowPriorityStopWatch.ElapsedMilliseconds >= 30000 || !_isExecuted)
            {
                _isExecuted = true;

                _lowPriorityStopWatch.Restart();

                try
                {
                    int realOnlineClientCount = Yupi.GetGame().GetClientManager().GetOnlineClients();
                    int clientCount = Yupi.GetGame().GetClientManager().ClientCount();

                    if (realOnlineClientCount != clientCount)
                        Writer.WriteLine("Number of Clients isn't Equal of Online Users. Running Analysis", "Yupi.Game",
                            ConsoleColor.DarkYellow);

                    if (realOnlineClientCount != clientCount)
                        Yupi.GetGame().GetClientManager().RemoveNotOnlineUsers();

                    int loadedRoomsCount = Yupi.GetGame().GetRoomManager().LoadedRoomsCount;

                    DateTime dateTime = new DateTime((DateTime.Now - Yupi.ServerStarted).Ticks);

                    Console.Title = string.Concat("Yupi | Time: ", int.Parse(dateTime.ToString("dd")) - 1, "d:",
                        dateTime.ToString("HH"), "h:", dateTime.ToString("mm"), "m | Conn: ", clientCount, " | Users: ",
                        realOnlineClientCount, " | Rooms: ", loadedRoomsCount);

                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        if (clientCount > _userPeak || realOnlineClientCount > _userPeak)
                            _userPeak = realOnlineClientCount;

                        commitableQueryReactor.RunFastQuery(string.Concat("UPDATE server_status SET stamp = '",
                            Yupi.GetUnixTimeStamp(), "', users_online = ", realOnlineClientCount, ", rooms_loaded = ",
                            loadedRoomsCount, ", server_ver = 'Yupi Emulator', userpeak = ", _userPeak));
                    }

                    Yupi.GetGame().GetNavigator().LoadNewPublicRooms();
                }
                catch (Exception e)
                {
                    ServerLogManager.LogException(e, MethodBase.GetCurrentMethod());
                }
            }
        }
    }
}