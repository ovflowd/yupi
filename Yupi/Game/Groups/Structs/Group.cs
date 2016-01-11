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

using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Groups.Composers;
using Yupi.Messages;

namespace Yupi.Game.Groups.Structs
{
    /// <summary>
    ///     Class Guild.
    /// </summary>
    internal class Group
    {
        /// <summary>
        ///     The admin only deco
        /// </summary>
        internal uint AdminOnlyDeco;

        /// <summary>
        ///     The admins
        /// </summary>
        internal Dictionary<uint, GroupMember> Admins;

        /// <summary>
        ///     The badge
        /// </summary>
        internal string Badge;

        /// <summary>
        ///     The colour1
        /// </summary>
        internal int Colour1;

        /// <summary>
        ///     The colour2
        /// </summary>
        internal int Colour2;

        /// <summary>
        ///     The create time
        /// </summary>
        internal int CreateTime;

        /// <summary>
        ///     The creator identifier
        /// </summary>
        internal uint CreatorId;

        /// <summary>
        ///     The description
        /// </summary>
        internal string Description;

        internal GroupForum Forum;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal uint Id;

        /// <summary>
        ///     The members
        /// </summary>
        internal Dictionary<uint, GroupMember> Members;

        /// <summary>
        ///     The name
        /// </summary>
        internal string Name;

        /// <summary>
        ///     The requests
        /// </summary>
        internal Dictionary<uint, GroupMember> Requests;

        /// <summary>
        ///     The room identifier
        /// </summary>
        internal uint RoomId;

        /// <summary>
        ///     The state
        /// </summary>
        internal uint State;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Group" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="create">The create.</param>
        /// <param name="creator">The creator.</param>
        /// <param name="colour1">The colour1.</param>
        /// <param name="colour2">The colour2.</param>
        /// <param name="members">The members.</param>
        /// <param name="requests">The requests.</param>
        /// <param name="admins">The admins.</param>
        /// <param name="state">The state.</param>
        /// <param name="adminOnlyDeco">The admin only deco.</param>
        /// <param name="forum"></param>
        internal Group(uint id, string name, string desc, uint roomId, string badge, int create, uint creator,
            int colour1, int colour2, Dictionary<uint, GroupMember> members, Dictionary<uint, GroupMember> requests,
            Dictionary<uint, GroupMember> admins, uint state, uint adminOnlyDeco, GroupForum forum)
        {
            Id = id;
            Name = name;
            Description = desc;
            RoomId = roomId;
            Badge = badge;
            CreateTime = create;
            CreatorId = creator;
            Colour1 = colour1 == 0 ? 1 : colour1;
            Colour2 = colour2 == 0 ? 1 : colour2;
            Members = members;
            Requests = requests;
            Admins = admins;
            State = state;
            AdminOnlyDeco = adminOnlyDeco;
            Forum = forum;
        }

        /// <summary>
        ///     Forums the data message.
        /// </summary>
        /// <param name="requesterId">The requester identifier.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage ForumDataMessage(uint requesterId)
        {
            return ForumDataMessageComposer.Compose(this, Forum, requesterId);
        }

        /// <summary>
        ///     Serializes the forum root.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void SerializeForumRoot(ServerMessage message)
        {
            ForumRootMessageComposer.Compose(message, this, Forum);
        }

        internal void CreateForum()
        {
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery(
                    "INSERT INTO groups_forums_data (group_id, forum_name, forum_description, forum_messages_count, forum_score, forum_lastposter_id, forum_lastposter_name, forum_lastposter_timestamp) " +
                    $"VALUES ('{Id}', @name, @desc, @msgcount, @score, @lastposterid, @lastpostername, @lasttimestamp)");
                adapter.AddParameter("name", Forum.ForumName);
                adapter.AddParameter("desc", Forum.ForumDescription);
                adapter.AddParameter("msgcount", Forum.ForumMessagesCount);
                adapter.AddParameter("score", Forum.ForumScore.ToString(CultureInfo.InvariantCulture));
                adapter.AddParameter("lastposterid", Forum.ForumLastPosterId);
                adapter.AddParameter("lastpostername", Forum.ForumLastPosterName);
                adapter.AddParameter("lasttimestamp", Forum.ForumLastPosterTimestamp);

                adapter.RunQuery();

                adapter.SetQuery($"SELECT * FROM groups_forums_data WHERE group_id='{Id}' LIMIT 1");

                DataRow row2 = adapter.GetRow();

                if (row2 == null)
                    Forum = new GroupForum(0, Forum.ForumName, Forum.ForumDescription, Forum.ForumMessagesCount, 0, 0,
                        string.Empty, 0, 0, 1, 1, 2);
                else
                    Forum = new GroupForum((uint) row2["id"], row2["forum_name"].ToString(),
                        row2["forum_description"].ToString(),
                        (uint) row2["forum_messages_count"], double.Parse(row2["forum_score"].ToString()),
                        (uint) row2["forum_lastposter_id"], row2["forum_lastposter_name"].ToString(),
                        (uint) row2["forum_lastposter_timestamp"],
                        (uint) row2["who_can_read"], (uint) row2["who_can_post"], (uint) row2["who_can_thread"],
                        (uint) row2["who_can_mod"]);
            }
        }

        /// <summary>
        ///     Updates the forum.
        /// </summary>
        internal void UpdateForum(bool createForum = false)
        {
            if (Forum.Id == 0 && !createForum)
                return;

            if (Forum.Id == 0 && createForum)
                CreateForum();

            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery(
                    $"UPDATE groups_forums_data SET forum_name = @name , forum_description = @desc , forum_messages_count = @msgcount , forum_score = @score , forum_lastposter_id = @lastposterid , forum_lastposter_name = @lastpostername , forum_lastposter_timestamp = @lasttimestamp WHERE group_id ={Id}");
                adapter.AddParameter("name", Forum.ForumName);
                adapter.AddParameter("desc", Forum.ForumDescription);
                adapter.AddParameter("msgcount", Forum.ForumMessagesCount);
                adapter.AddParameter("score", Forum.ForumScore.ToString(CultureInfo.InvariantCulture));
                adapter.AddParameter("lastposterid", Forum.ForumLastPosterId);
                adapter.AddParameter("lastpostername", Forum.ForumLastPosterName);
                adapter.AddParameter("lasttimestamp", Forum.ForumLastPosterTimestamp);

                adapter.RunQuery();
            }
        }
    }
}