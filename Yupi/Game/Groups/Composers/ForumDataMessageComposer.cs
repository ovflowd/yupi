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

using Yupi.Game.Groups.Structs;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Groups.Composers
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
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(Group group, GroupForum groupForum, uint requesterId)
        {
            string string1 = string.Empty, string2 = string.Empty, string3 = string.Empty, string4 = string.Empty;

            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("GroupForumDataMessageComposer"));

            message.AppendInteger(group.Id);
            message.AppendString(group.Name);
            message.AppendString(group.Description);
            message.AppendString(group.Badge);
            message.AppendInteger(0);
            message.AppendInteger(0);
            message.AppendInteger(groupForum.ForumMessagesCount);
            message.AppendInteger(0);
            message.AppendInteger(0);
            message.AppendInteger(groupForum.ForumLastPosterId);
            message.AppendString(groupForum.ForumLastPosterName);
            message.AppendInteger(groupForum.ForumLastPostTime);
            message.AppendInteger(groupForum.WhoCanRead);
            message.AppendInteger(groupForum.WhoCanPost);
            message.AppendInteger(groupForum.WhoCanThread);
            message.AppendInteger(groupForum.WhoCanMod);

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

            message.AppendString(string1);
            message.AppendString(string2);
            message.AppendString(string3);
            message.AppendString(string4);
            message.AppendString(string.Empty);
            message.AppendBool(requesterId == group.CreatorId);
            message.AppendBool(true);

            return message;
        }
    }
}