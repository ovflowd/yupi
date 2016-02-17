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
using Yupi.Game.Groups.Structs;
using Yupi.Messages;

namespace Yupi.Game.Groups.Composers
{
    /// <summary>
    ///     Class ForumRootMessageComposer.
    /// </summary>
    internal class ForumRootMessageComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="group"></param>
        /// <param name="groupForum"></param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(ServerMessage message, Group group, GroupForum groupForum)
        {
            message.AppendInteger(group.Id);
            message.AppendString(group.Name);
            message.AppendString(string.Empty);
            message.AppendString(group.Badge);
            message.AppendInteger(0);
            message.AppendInteger((int) Math.Round(groupForum.ForumScore));
            message.AppendInteger(groupForum.ForumMessagesCount);
            message.AppendInteger(0);
            message.AppendInteger(0);
            message.AppendInteger(groupForum.ForumLastPosterId);
            message.AppendString(groupForum.ForumLastPosterName);
            message.AppendInteger(groupForum.ForumLastPostTime);

            return message;
        }
    }
}