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

namespace Yupi.Emulator.Data
{
    /// <summary>
    ///     Class ServerStatusUpdater.
    /// </summary>
     class ServerStatusUpdater
    {
        /// <summary>
        ///     Server Status Stop Watch
        /// </summary>
        private static Stopwatch _refreshInterval;

        /// <summary>
        ///    Load Server Status Stop Watch
        /// </summary>
         static void Load()
        {
            _refreshInterval = new Stopwatch();

            _refreshInterval.Start();
        }

        /// <summary>
        ///     Updates Yupi Environment Console Title Status
        /// </summary>
         static void Process()
        {
            if (_refreshInterval.ElapsedMilliseconds >= 10000)
            {
                DateTime dateTime = new DateTime((DateTime.Now - Yupi.YupiServerStartDateTime).Ticks);

                Console.Title = string.Concat("Yupi | Time: ", int.Parse(dateTime.ToString("dd")) - 1, "d:", dateTime.ToString("HH"), "h:", dateTime.ToString("mm"), "m | Users: ", Yupi.GetGame().GetClientManager().GetOnlineClients(), " | Rooms: ", Yupi.GetGame().GetRoomManager().LoadedRoomsCount);

                _refreshInterval.Restart();
            }
        }
    }
}
