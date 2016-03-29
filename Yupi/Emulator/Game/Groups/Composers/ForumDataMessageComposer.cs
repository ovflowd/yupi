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

using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Groups.Composers
{
    /// <summary>
    ///     Class ForumDataMessageComposer.
    /// </summary>
    internal class ForumDataMessageComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="groupForum"></param>
        /// <param name="requesterId"></param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal static SimpleServerMessageBuffer Compose(Group group, GroupForum groupForum, uint requesterId)
        {
            string string1 = string.Empty, string2 = string.Empty, string3 = string.Empty, string4 = string.Empty;

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GroupForumDataMessageComposer"));

            messageBuffer.AppendInteger(group.Id);
            messageBuffer.AppendString(group.Name);
            messageBuffer.AppendString(group.Description);
            messageBuffer.AppendString(group.Badge);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(groupForum.ForumMessagesCount);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(groupForum.ForumLastPosterId);
            messageBuffer.AppendString(groupForum.ForumLastPosterName);
            messageBuffer.AppendInteger(groupForum.ForumLastPostTime);
            messageBuffer.AppendInteger(groupForum.WhoCanRead);
            messageBuffer.AppendInteger(groupForum.WhoCanPost);
            messageBuffer.AppendInteger(groupForum.WhoCanThread);
            messageBuffer.AppendInteger(groupForum.WhoCanMod);

            if (groupForum.WhoCanRead == 1 && !group.Members.ContainsKey(requesterId))
                string1 = "not_member";
            if (groupForum.WhoCanRead == 2 && !group.Admins.ContainsKey(requesterId))
                string1 = "not_admin";
            if (groupForum.WhoCanPost == 1 && !group.Members.ContainsKey(requesterId))
                string2 = "not_member";
            if (groupForum.WhoCanPost == 2 && !group.Admins.ContainsKey(requesterId))
                string2 = "not_admin";
            if (groupForum.WhoCanPost == 3 && requesterId != group.CreatorId)
                string2 = "not_owner";
            if (groupForum.WhoCanThread == 1 && !group.Members.ContainsKey(requesterId))
                string3 = "not_member";
            if (groupForum.WhoCanThread == 2 && !group.Admins.ContainsKey(requesterId))
                string3 = "not_admin";
            if (groupForum.WhoCanThread == 3 && requesterId != group.CreatorId)
                string3 = "not_owner";
            if (groupForum.WhoCanMod == 2 && !group.Admins.ContainsKey(requesterId))
                string4 = "not_admin";
            if (groupForum.WhoCanMod == 3 && requesterId != group.CreatorId)
                string4 = "not_owner";

            messageBuffer.AppendString(string1);
            messageBuffer.AppendString(string2);
            messageBuffer.AppendString(string3);
            messageBuffer.AppendString(string4);
            messageBuffer.AppendString(string.Empty);
            messageBuffer.AppendBool(requesterId == group.CreatorId);
            messageBuffer.AppendBool(true);

            return messageBuffer;
        }
    }
}