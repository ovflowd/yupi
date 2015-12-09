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
                    var clientCount = Yupi.GetGame().GetClientManager().ClientCount();
                    var loadedRoomsCount = Yupi.GetGame().GetRoomManager().LoadedRoomsCount;
                    var dateTime = new DateTime((DateTime.Now - Yupi.ServerStarted).Ticks);

                    Console.Title = string.Concat("YupiEmulator v" + Yupi.Version + "." + Yupi.Build + " | TIME: ",
                        int.Parse(dateTime.ToString("dd")) - 1 + dateTime.ToString(":HH:mm:ss"), " | ONLINE COUNT: ",
                        clientCount, " | ROOM COUNT: ", loadedRoomsCount);
                    using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
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