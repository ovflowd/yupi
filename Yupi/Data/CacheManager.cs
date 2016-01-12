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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.UI;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Users;

namespace Yupi.Data
{
    /// <summary>
    ///     Class CacheManager.
    /// </summary>
    public static class CacheManager
    {
        /// <summary>
        ///     The _thread
        /// </summary>
        private static Thread _thread;

        /// <summary>
        ///     The working
        /// </summary>
        public static bool Working;

        /// <summary>
        ///     Starts the process.
        /// </summary>
        public static void StartProcess()
        {
            _thread = new Thread(Process) {Name = "Cache Thread"};

            _thread.Start();

            Working = true;
        }

        /// <summary>
        ///     Stops the process.
        /// </summary>
        public static void StopProcess()
        {
            _thread.Abort();
            Working = false;
        }

        /// <summary>
        ///     Processes this instance.
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
        ///     Clears the user cache.
        /// </summary>
        private static void ClearUserCache()
        {
            List<uint> toRemove = new List<uint>();

            foreach (KeyValuePair<uint, Habbo> user in Yupi.UsersCached)
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

            foreach (uint userId in toRemove)
            {
                Habbo nullHabbo; 

                if (Yupi.UsersCached.ContainsKey(userId))
                    Yupi.UsersCached.TryRemove(userId, out nullHabbo);
            }
        }

        /// <summary>
        ///     Clears the rooms cache.
        /// </summary>
        private static void ClearRoomsCache()
        {
            if (Yupi.GetGame() == null || Yupi.GetGame().GetRoomManager() == null || Yupi.GetGame().GetRoomManager().LoadedRoomData == null)
                return;

            List<uint> toRemove = (from roomData in Yupi.GetGame().GetRoomManager().LoadedRoomData
                where roomData.Value != null && roomData.Value.UsersNow <= 0
                where !((DateTime.Now - roomData.Value.LastUsed).TotalMilliseconds < 1800000)
                select roomData.Key).ToList();

            foreach (uint roomId in toRemove)
            {
                RoomData nullRoom;

                if (Yupi.GetGame().GetRoomManager().LoadedRoomData.ContainsKey(roomId))
                    Yupi.GetGame().GetRoomManager().LoadedRoomData.TryRemove(roomId, out nullRoom);
            }
        }
    }
}