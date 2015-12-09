using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Users;

namespace Yupi.Data
{
    /// <summary>
    /// Class CacheManager.
    /// </summary>
    public static class CacheManager
    {
        /// <summary>
        /// The _thread
        /// </summary>
        private static Thread _thread;
        /// <summary>
        /// The working
        /// </summary>
        public static bool Working;

        /// <summary>
        /// Starts the process.
        /// </summary>
        public static void StartProcess()
        {
            _thread = new Thread(Process) { Name = "Cache Thread" };
            _thread.Start();
            Working = true;
        }

        /// <summary>
        /// Stops the process.
        /// </summary>
        public static void StopProcess()
        {
            _thread.Abort();
            Working = false;
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        private static void Process()
        {
            while (Working)
            {
                ClearUserCache();
                ClearRoomsCache();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Thread.Sleep(1000000); // WTF? <<< #TODO WTF!!
            }
        }

        /// <summary>
        /// Clears the user cache.
        /// </summary>
        private static void ClearUserCache()
        {
            var toRemove = new List<uint>();

            foreach (var user in Yupi.UsersCached)
            {
                if (user.Value == null)
                {
                    toRemove.Add(user.Key);
                    return;
                }

                if (Yupi.GetGame().GetClientManager().Clients.ContainsKey(user.Key))
                    continue;

                if ((DateTime.Now - user.Value.LastUsed).TotalMilliseconds < 1800000)
                    continue;

                toRemove.Add(user.Key);
            }

            foreach (var userId in toRemove)
            {
                Habbo nullHabbo;

                if (Yupi.UsersCached.TryRemove(userId, out nullHabbo))
                    nullHabbo = null;
            }
        }

        /// <summary>
        /// Clears the rooms cache.
        /// </summary>
        private static void ClearRoomsCache()
        {
            if (Yupi.GetGame() == null || Yupi.GetGame().GetRoomManager() == null || Yupi.GetGame().GetRoomManager().LoadedRoomData == null)
                return;

            var toRemove = (from roomData in Yupi.GetGame().GetRoomManager().LoadedRoomData where roomData.Value != null && roomData.Value.UsersNow <= 0 where !((DateTime.Now - roomData.Value.LastUsed).TotalMilliseconds < 1800000) select roomData.Key).ToList();

            foreach (var roomId in toRemove)
            {
                RoomData nullRoom;

                if (Yupi.GetGame().GetRoomManager().LoadedRoomData.TryRemove(roomId, out nullRoom))
                    nullRoom = null;
            }
        }
    }
}