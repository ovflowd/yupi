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
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Groups.Composers
{
    /// <summary>
    ///     Class ForumRootMessageComposer.
    /// </summary>
     class ForumRootMessageComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="group"></param>
        /// <param name="groupForum"></param>
        /// <returns>SimpleServerMessageBuffer.</returns>
         static SimpleServerMessageBuffer Compose(SimpleServerMessageBuffer messageBuffer, Group group, GroupForum groupForum)
        {
            messageBuffer.AppendInteger(group.Id);
            messageBuffer.AppendString(group.Name);
            messageBuffer.AppendString(string.Empty);
            messageBuffer.AppendString(group.Badge);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger((int) Math.Round(groupForum.ForumScore));
            messageBuffer.AppendInteger(groupForum.ForumMessagesCount);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(groupForum.ForumLastPosterId);
            messageBuffer.AppendString(groupForum.ForumLastPosterName);
            messageBuffer.AppendInteger(groupForum.ForumLastPostTime);

            return messageBuffer;
        }
    }
}