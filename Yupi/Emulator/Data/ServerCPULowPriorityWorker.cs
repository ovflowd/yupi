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
using Yupi.Core.Io.Logger;

namespace Yupi.Data
{
    /// <summary>
    ///     Class ServerCpuLowPriorityWorker.
    /// </summary>
    internal class ServerCpuLowPriorityWorker
    {
         /// <summary>
        ///     If Task is Executed
        /// </summary>
        private static bool _isExecuted;
        
        /// <summary>
        ///     Stop Watch Executer
        /// </summary>
        private static Stopwatch _lowPriorityStopWatch;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        internal static void Load()
        {
             _lowPriorityStopWatch = new Stopwatch();
             
             _lowPriorityStopWatch.Start();
        }

        /// <summary>
        ///     Processes the specified caller.
        /// </summary>
        internal static void Process()
        {
            if (_lowPriorityStopWatch.ElapsedMilliseconds >= 10000 || !_isExecuted)
            {
                try
                {
                    DateTime dateTime = new DateTime((DateTime.Now - Yupi.YupiServerStartDateTime).Ticks);

                    Console.Title = string.Concat("Yupi | Time: ", int.Parse(dateTime.ToString("dd")) - 1, "d:", dateTime.ToString("HH"), "h:", dateTime.ToString("mm"), "m | Users: ", Yupi.GetGame().GetClientManager().ClientCount(), " | Rooms: ", Yupi.GetGame().GetRoomManager().LoadedRoomsCount);

                    Yupi.GetGame().GetNavigator().LoadNewPublicRooms();
                }
                catch (Exception e)
                {
                    YupiLogManager.LogException(e, "Failed Processing LowPriorityWorker.");
                }
                finally
                {
                     _isExecuted = true;
                     
                     _lowPriorityStopWatch.Restart();
                }
            }
        }
    }
}
