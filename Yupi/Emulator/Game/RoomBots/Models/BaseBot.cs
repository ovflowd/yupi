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

using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.RoomBots.Interfaces;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.RoomBots.Models
{
     class BaseBot : BotAi
    {
        /// <summary>
        ///     Called when [self enter room].
        /// </summary>
         override void OnSelfEnterRoom()
        {
        }

        /// <summary>
        ///     Called when [self leave room].
        /// </summary>
        /// <param name="kicked">if set to <c>true</c> [kicked].</param>
         override void OnSelfLeaveRoom(bool kicked)
        {
        }

        /// <summary>
        ///     Called when [user enter room].
        /// </summary>
        /// <param name="user">The user.</param>
         override void OnUserEnterRoom(RoomUser user)
        {
        }

        /// <summary>
        ///     Called when [user leave room].
        /// </summary>
        /// <param name="client">The client.</param>
         override void OnUserLeaveRoom(GameClient client)
        {
        }

        /// <summary>
        ///     Called when [user say].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="msg">The MSG.</param>
         override void OnUserSay(RoomUser user, string msg)
        {
        }

        /// <summary>
        ///     Called when [user shout].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
         override void OnUserShout(RoomUser user, string message)
        {
        }

        /// <summary>
        ///     Called when [timer tick].
        /// </summary>
         override void OnTimerTick()
        {
        }

         override void OnChatTick()
        {
        }

        /// <summary>
        ///     Modifieds this instance.
        /// </summary>
         override void Modified()
        {
        }
    }
}