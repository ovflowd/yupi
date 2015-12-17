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
using Yupi.Core.Io;
using Yupi.Data.Base.Sessions.Interfaces;

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
                    int clientCount = Yupi.GetGame().GetClientManager().ClientCount();
                    int loadedRoomsCount = Yupi.GetGame().GetRoomManager().LoadedRoomsCount;
                    DateTime dateTime = new DateTime((DateTime.Now - Yupi.ServerStarted).Ticks);

                    Console.Title = string.Concat("Yupi | UpTime: ",
                        int.Parse(dateTime.ToString("dd")) - 1 + dateTime.ToString(":HH:mm:ss"), " | Users: ",
                        clientCount, " | Rooms: ", loadedRoomsCount);

                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        if (clientCount > _userPeak)
                            _userPeak = clientCount;

                        queryReactor.RunFastQuery(string.Concat("UPDATE server_status SET stamp = '",
                            Yupi.GetUnixTimeStamp(), "', users_online = ", clientCount, ", rooms_loaded = ",
                            loadedRoomsCount, ", server_ver = 'Yupi Emulator', userpeak = ", _userPeak));
                    }

                    Yupi.GetGame().GetNavigator().LoadNewPublicRooms();
                }
                catch (Exception e)
                {
                    Writer.LogException(e.ToString());
                }
            }
        }
    }
}