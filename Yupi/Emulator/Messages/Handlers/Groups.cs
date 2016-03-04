using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Catalogs.Composers;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class MessageHandler.
    /// </summary>
    internal partial class MessageHandler
    {
        internal readonly ushort TotalPerPage = 20;

        /// <summary>
        ///     Serializes the group purchase page.
        /// </summary>
        internal void SerializeGroupPurchasePage()
        {
            HashSet<RoomData> list = new HashSet<RoomData>(Session.GetHabbo().UsersRooms.Where(x => x.Group == null));

            Response.Init(PacketLibraryManager.OutgoingHandler("GroupPurchasePageMessageComposer"));
            Response.AppendInteger(10);
            Response.AppendInteger(list.Count);

            foreach (RoomData current2 in list)
            {
                Response.AppendInteger(current2.Id);
                Response.AppendString(current2.Name);
                Response.AppendBool(false);
            }

            Response.AppendInteger(5);
            Response.AppendInteger(10);
            Response.AppendInteger(3);
            Response.AppendInteger(4);
            Response.AppendInteger(25);
            Response.AppendInteger(17);
            Response.AppendInteger(5);
            Response.AppendInteger(25);
            Response.AppendInteger(17);
            Response.AppendInteger(3);
            Response.AppendInteger(29);
            Response.AppendInteger(11);
            Response.AppendInteger(4);
            Response.AppendInteger(0);
            Response.AppendInteger(0);
            Response.AppendInteger(0);
            SendResponse();
        }

        /// <summary>
        ///     Serializes the group purchase parts.
        /// </summary>
        internal void SerializeGroupPurchaseParts()
        {
            Response.Init(PacketLibraryManager.OutgoingHandler("GroupPurchasePartsMessageComposer"));
            Response.AppendInteger(Yupi.GetGame().GetGroupManager().Bases.Count);

            foreach (GroupBases current in Yupi.GetGame().GetGroupManager().Bases)
            {
                Response.AppendInteger(current.Id);
                Response.AppendString(current.Value1);
                Response.AppendString(current.Value2);
            }

            Response.AppendInteger(Yupi.GetGame().GetGroupManager().Symbols.Count);

            foreach (GroupSymbols current2 in Yupi.GetGame().GetGroupManager().Symbols)
            {
                Response.AppendInteger(current2.Id);
                Response.AppendString(current2.Value1);
                Response.AppendString(current2.Value2);
            }

            Response.AppendInteger(Yupi.GetGame().GetGroupManager().BaseColours.Count);

            foreach (GroupBaseColours current3 in Yupi.GetGame().GetGroupManager().BaseColours)
            {
                Response.AppendInteger(current3.Id);
                Response.AppendString(current3.Colour);
            }

            Response.AppendInteger(Yupi.GetGame().GetGroupManager().SymbolColours.Count);

            foreach (GroupSymbolColours current4 in Yupi.GetGame().GetGroupManager().SymbolColours.Values)
            {
                Response.AppendInteger(current4.Id);
                Response.AppendString(current4.Colour);
            }

            Response.AppendInteger(Yupi.GetGame().GetGroupManager().BackGroundColours.Count);

            foreach (GroupBackGroundColours current5 in Yupi.GetGame().GetGroupManager().BackGroundColours.Values)
            {
                Response.AppendInteger(current5.Id);
                Response.AppendString(current5.Colour);
            }

            SendResponse();
        }

        /// <summary>
        ///     Purchases the group.
        /// </summary>
        internal void PurchaseGroup()
        {
            if (Session == null || Session.GetHabbo().Credits < 10)
                return;

            List<int> gStates = new List<int>();
            string name = Request.GetString();
            string description = Request.GetString();
            uint roomid = Request.GetUInteger();
            int color = Request.GetInteger();
            int num3 = Request.GetInteger();

            Request.GetInteger();

            int guildBase = Request.GetInteger();
            int guildBaseColor = Request.GetInteger();
            int num6 = Request.GetInteger();
            RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomid);

            if (roomData.Owner != Session.GetHabbo().UserName)
                return;

            for (int i = 0; i < num6*3; i++)
                gStates.Add(Request.GetInteger());

            string image = Yupi.GetGame().GetGroupManager().GenerateGuildImage(guildBase, guildBaseColor, gStates);

            Group theGroup;

            Yupi.GetGame()
                .GetGroupManager()
                .CreateGroup(name, description, roomid, image, Session,
                    !Yupi.GetGame().GetGroupManager().SymbolColours.Contains(color) ? 1 : color,
                    !Yupi.GetGame().GetGroupManager().BackGroundColours.Contains(num3) ? 1 : num3, out theGroup);

            Session.SendMessage(CatalogPageComposer.PurchaseOk(0u, "CREATE_GUILD", 10));
            Response.Init(PacketLibraryManager.OutgoingHandler("GroupRoomMessageComposer"));
            Response.AppendInteger(roomid);
            Response.AppendInteger(theGroup.Id);
            SendResponse();
            roomData.Group = theGroup;
            roomData.GroupId = theGroup.Id;
            roomData.SerializeRoomData(Response, Session, true);

            if (!Session.GetHabbo().InRoom || Session.GetHabbo().CurrentRoom.RoomId != roomData.Id)
            {
                Session.GetMessageHandler().PrepareRoomForUser(roomData.Id, roomData.PassWord);
                Session.GetHabbo().CurrentRoomId = roomData.Id;
            }

            if (Session.GetHabbo().CurrentRoom != null &&
                !Session.GetHabbo().CurrentRoom.LoadedGroups.ContainsKey(theGroup.Id))
                Session.GetHabbo().CurrentRoom.LoadedGroups.Add(theGroup.Id, theGroup.Badge);

            if (CurrentLoadingRoom != null && !CurrentLoadingRoom.LoadedGroups.ContainsKey(theGroup.Id))
                CurrentLoadingRoom.LoadedGroups.Add(theGroup.Id, theGroup.Badge);

            if (CurrentLoadingRoom != null)
            {
                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomGroupMessageComposer"));

                simpleServerMessageBuffer.AppendInteger(CurrentLoadingRoom.LoadedGroups.Count);

                foreach (KeyValuePair<uint, string> current in CurrentLoadingRoom.LoadedGroups)
                {
                    simpleServerMessageBuffer.AppendInteger(current.Key);
                    simpleServerMessageBuffer.AppendString(current.Value);
                }

                CurrentLoadingRoom.SendMessage(simpleServerMessageBuffer);
            }

            if (CurrentLoadingRoom == null || Session.GetHabbo().FavouriteGroup != theGroup.Id)
                return;

            SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ChangeFavouriteGroupMessageComposer"));

            simpleServerMessage2.AppendInteger(
                CurrentLoadingRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id).VirtualId);
            simpleServerMessage2.AppendInteger(theGroup.Id);
            simpleServerMessage2.AppendInteger(3);
            simpleServerMessage2.AppendString(theGroup.Name);

            CurrentLoadingRoom.SendMessage(simpleServerMessage2);
        }

        /// <summary>
        ///     Serializes the group information.
        /// </summary>
        internal void SerializeGroupInfo()
        {
            uint groupId = Request.GetUInteger();
            bool newWindow = Request.GetBool();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (group == null)
                return;

            Yupi.GetGame().GetGroupManager().SerializeGroupInfo(group, Response, Session, newWindow);
        }

        /// <summary>
        ///     Serializes the group members.
        /// </summary>
        internal void SerializeGroupMembers()
        {
            uint groupId = Request.GetUInteger();
            int page = Request.GetInteger();
            string searchVal = Request.GetString();
            uint reqType = Request.GetUInteger();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            Response.Init(PacketLibraryManager.OutgoingHandler("GroupMembersMessageComposer"));

            Yupi.GetGame().GetGroupManager().SerializeGroupMembers(Response, group, reqType, Session, searchVal, page);

            SendResponse();
        }

        /// <summary>
        ///     Makes the group admin.
        /// </summary>
        internal void MakeGroupAdmin()
        {
            uint num = Request.GetUInteger();
            uint num2 = Request.GetUInteger();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(num);

            if (Session.GetHabbo().Id != group.CreatorId || !group.Members.ContainsKey(num2) ||
                group.Admins.ContainsKey(num2))
                return;

            group.Members[num2].Rank = 1;

            group.Admins.Add(num2, group.Members[num2]);

            Response.Init(PacketLibraryManager.OutgoingHandler("GroupMembersMessageComposer"));
            Yupi.GetGame().GetGroupManager().SerializeGroupMembers(Response, group, 1u, Session);

            SendResponse();

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(num2).UserName);

            if (roomUserByHabbo != null)
            {
                if (!roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
                    roomUserByHabbo.AddStatus("flatctrl 1", "");

                Response.Init(PacketLibraryManager.OutgoingHandler("RoomRightsLevelMessageComposer"));
                Response.AppendInteger(1);
                roomUserByHabbo.GetClient().SendMessage(GetResponse());
                roomUserByHabbo.UpdateNeeded = true;
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("UPDATE groups_members SET rank='1' WHERE group_id=",
                    num, " AND user_id=", num2, " LIMIT 1;"));
        }

        /// <summary>
        ///     Removes the group admin.
        /// </summary>
        internal void RemoveGroupAdmin()
        {
            uint num = Request.GetUInteger();
            uint num2 = Request.GetUInteger();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(num);

            if (Session.GetHabbo().Id != group.CreatorId || !group.Members.ContainsKey(num2) ||
                !group.Admins.ContainsKey(num2))
                return;

            group.Members[num2].Rank = 0;
            group.Admins.Remove(num2);

            Response.Init(PacketLibraryManager.OutgoingHandler("GroupMembersMessageComposer"));
            Yupi.GetGame().GetGroupManager().SerializeGroupMembers(Response, group, 0u, Session);
            SendResponse();

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);
            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(num2).UserName);

            if (roomUserByHabbo != null)
            {
                if (roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
                    roomUserByHabbo.RemoveStatus("flatctrl 1");

                Response.Init(PacketLibraryManager.OutgoingHandler("RoomRightsLevelMessageComposer"));
                Response.AppendInteger(0);
                roomUserByHabbo.GetClient().SendMessage(GetResponse());
                roomUserByHabbo.UpdateNeeded = true;
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("UPDATE groups_members SET rank='0' WHERE group_id=",
                    num, " AND user_id=", num2, " LIMIT 1;"));
        }

        /// <summary>
        ///     Accepts the membership.
        /// </summary>
        internal void AcceptMembership()
        {
            uint groupId = Request.GetUInteger();
            uint userId = Request.GetUInteger();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (Session.GetHabbo().Id != group.CreatorId && !group.Admins.ContainsKey(Session.GetHabbo().Id) &&
                !group.Requests.ContainsKey(userId))
                return;

            if (group.Members.ContainsKey(userId))
            {
                group.Requests.Remove(userId);

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery(
                        $"DELETE FROM groups_requests WHERE group_id = '{groupId}' AND user_id = '{userId}' LIMIT 1");
                return;
            }

            GroupMember memberGroup = group.Requests[userId];

            memberGroup.DateJoin = Yupi.GetUnixTimeStamp();
            group.Members.Add(userId, memberGroup);
            group.Requests.Remove(userId);
            group.Admins.Add(userId, group.Members[userId]);

            Yupi.GetGame().GetGroupManager().SerializeGroupInfo(group, Response, Session);
            Response.Init(PacketLibraryManager.OutgoingHandler("GroupMembersMessageComposer"));
            Yupi.GetGame().GetGroupManager().SerializeGroupMembers(Response, group, 0u, Session);
            SendResponse();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"DELETE FROM groups_requests WHERE group_id = '{groupId}' AND user_id = '{userId}' LIMIT 1");

            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                queryreactor2.RunFastQuery(
                    $"INSERT INTO groups_members (group_id, user_id, rank, date_join) VALUES ('{groupId}','{userId}','0','{Yupi.GetUnixTimeStamp()}')");
        }

        /// <summary>
        ///     Declines the membership.
        /// </summary>
        internal void DeclineMembership()
        {
            uint groupId = Request.GetUInteger();
            uint userId = Request.GetUInteger();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (Session.GetHabbo().Id != group.CreatorId && !group.Admins.ContainsKey(Session.GetHabbo().Id) &&
                !group.Requests.ContainsKey(userId))
                return;

            group.Requests.Remove(userId);

            Response.Init(PacketLibraryManager.OutgoingHandler("GroupMembersMessageComposer"));
            Yupi.GetGame().GetGroupManager().SerializeGroupMembers(Response, group, 2u, Session);
            SendResponse();

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(userId).UserName);

            if (roomUserByHabbo != null)
            {
                if (roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
                    roomUserByHabbo.RemoveStatus("flatctrl 1");

                roomUserByHabbo.UpdateNeeded = true;
            }

            Yupi.GetGame().GetGroupManager().SerializeGroupInfo(group, Response, Session);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery("DELETE FROM groups_requests WHERE group_id=" + groupId +
                                          " AND user_id=" + userId);
        }

        /// <summary>
        ///     Joins the group.
        /// </summary>
        internal void JoinGroup()
        {
            uint groupId = Request.GetUInteger();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);
            Habbo user = Session.GetHabbo();

            if (!group.Members.ContainsKey(user.Id))
            {
                if (group.State == 0)
                {
                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.RunFastQuery(
                            string.Concat("INSERT INTO groups_members (user_id, group_id, date_join) VALUES (", user.Id,
                                ",", groupId, ",", Yupi.GetUnixTimeStamp(), ")"));
                        queryReactor.RunFastQuery(string.Concat("UPDATE users_stats SET favourite_group=",
                            groupId, " WHERE id= ", user.Id, " LIMIT 1"));
                    }

                    group.Members.Add(user.Id,
                        new GroupMember(user.Id, user.UserName, user.Look, group.Id, 0, Yupi.GetUnixTimeStamp()));

                    Session.GetHabbo().UserGroups.Add(group.Members[user.Id]);
                }
                else
                {
                    if (!group.Requests.ContainsKey(user.Id))
                    {
                        using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                            queryreactor2.RunFastQuery(
                                string.Concat("INSERT INTO groups_requests (user_id, group_id) VALUES (",
                                    Session.GetHabbo().Id, ",", groupId, ")"));

                        GroupMember groupRequest = new GroupMember(user.Id, user.UserName, user.Look, group.Id, 0,
                            Yupi.GetUnixTimeStamp());

                        group.Requests.Add(user.Id, groupRequest);
                    }
                }

                Yupi.GetGame().GetGroupManager().SerializeGroupInfo(group, Response, Session);
            }
        }

        /// <summary>
        ///     Makes the fav.
        /// </summary>
        internal void MakeFav()
        {
            uint groupId = Request.GetUInteger();

            Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (theGroup == null)
                return;

            if (!theGroup.Members.ContainsKey(Session.GetHabbo().Id))
                return;

            Session.GetHabbo().FavouriteGroup = theGroup.Id;
            Yupi.GetGame().GetGroupManager().SerializeGroupInfo(theGroup, Response, Session);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("UPDATE users_stats SET favourite_group =",
                    theGroup.Id, " WHERE id=", Session.GetHabbo().Id, " LIMIT 1;"));

            Response.Init(PacketLibraryManager.OutgoingHandler("FavouriteGroupMessageComposer"));
            Response.AppendInteger(Session.GetHabbo().Id);
            Session.SendMessage(Response);

            if (Session.GetHabbo().CurrentRoom != null)
            {
                if (!Session.GetHabbo().CurrentRoom.LoadedGroups.ContainsKey(theGroup.Id))
                {
                    Session.GetHabbo().CurrentRoom.LoadedGroups.Add(theGroup.Id, theGroup.Badge);
                    Response.Init(PacketLibraryManager.OutgoingHandler("RoomGroupMessageComposer"));
                    Response.AppendInteger(Session.GetHabbo().CurrentRoom.LoadedGroups.Count);

                    foreach (KeyValuePair<uint, string> current in Session.GetHabbo().CurrentRoom.LoadedGroups)
                    {
                        Response.AppendInteger(current.Key);
                        Response.AppendString(current.Value);
                    }

                    Session.GetHabbo().CurrentRoom.SendMessage(Response);
                }
            }

            Response.Init(PacketLibraryManager.OutgoingHandler("ChangeFavouriteGroupMessageComposer"));
            Response.AppendInteger(0);
            Response.AppendInteger(theGroup.Id);
            Response.AppendInteger(3);
            Response.AppendString(theGroup.Name);

            Session.SendMessage(Response);
        }

        /// <summary>
        ///     Removes the fav.
        /// </summary>
        internal void RemoveFav()
        {
            Request.GetUInteger();
            Session.GetHabbo().FavouriteGroup = 0;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"UPDATE users_stats SET favourite_group=0 WHERE id={Session.GetHabbo().Id} LIMIT 1;");

            Response.Init(PacketLibraryManager.OutgoingHandler("FavouriteGroupMessageComposer"));
            Response.AppendInteger(Session.GetHabbo().Id);
            Session.SendMessage(Response);
            Response.Init(PacketLibraryManager.OutgoingHandler("ChangeFavouriteGroupMessageComposer"));
            Response.AppendInteger(0);
            Response.AppendInteger(-1);
            Response.AppendInteger(-1);
            Response.AppendString(string.Empty);

            Session.SendMessage(Response);
        }

        /// <summary>
        ///     Publishes the forum thread.
        /// </summary>
        internal void PublishForumThread()
        {
            if (Yupi.GetUnixTimeStamp() - Session.GetHabbo().LastSqlQuery < 20)
                return;

            uint groupId = Request.GetUInteger();
            uint threadId = Request.GetUInteger();
            string subject = Request.GetString();
            string content = Request.GetString();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (group == null || group.Forum.Id == 0)
                return;

            int timestamp = Yupi.GetUnixTimeStamp();

            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                if (threadId != 0)
                {
                    dbClient.SetQuery($"SELECT * FROM groups_forums_posts WHERE id = {threadId}");

                    DataRow row = dbClient.GetRow();
                    GroupForumPost post = new GroupForumPost(row);

                    if (post.Locked || post.Hidden)
                    {
                        Session.SendNotif(Yupi.GetLanguage().GetVar("forums_cancel"));
                        return;
                    }
                }

                Session.GetHabbo().LastSqlQuery = Yupi.GetUnixTimeStamp();
                dbClient.SetQuery(
                    "INSERT INTO groups_forums_posts (group_id, parent_id, timestamp, poster_id, poster_name, poster_look, subject, post_content) VALUES (@gid, @pard, @ts, @pid, @pnm, @plk, @subjc, @content)");
                dbClient.AddParameter("gid", groupId);
                dbClient.AddParameter("pard", threadId);
                dbClient.AddParameter("ts", timestamp);
                dbClient.AddParameter("pid", Session.GetHabbo().Id);
                dbClient.AddParameter("pnm", Session.GetHabbo().UserName);
                dbClient.AddParameter("plk", Session.GetHabbo().Look);
                dbClient.AddParameter("subjc", subject);
                dbClient.AddParameter("content", content);

                threadId = (uint) dbClient.GetInteger();
            }

            group.Forum.ForumScore += 0.25;
            group.Forum.ForumLastPosterName = Session.GetHabbo().UserName;
            group.Forum.ForumLastPosterId = Session.GetHabbo().Id;
            group.Forum.ForumLastPosterTimestamp = (uint) timestamp;
            group.Forum.ForumMessagesCount++;
            group.UpdateForum();

            if (threadId == 0)
            {
                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GroupForumNewThreadMessageComposer"));
                messageBuffer.AppendInteger(groupId);
                messageBuffer.AppendInteger(threadId);
                messageBuffer.AppendInteger(Session.GetHabbo().Id);
                messageBuffer.AppendString(subject);
                messageBuffer.AppendString(content);
                messageBuffer.AppendBool(false);
                messageBuffer.AppendBool(false);
                messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - timestamp);
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendString(string.Empty);
                messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - timestamp);
                messageBuffer.AppendByte(1);
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendString(string.Empty);
                messageBuffer.AppendInteger(42);
                Session.SendMessage(messageBuffer);
            }
            else
            {
                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GroupForumNewResponseMessageComposer"));
                messageBuffer.AppendInteger(groupId);
                messageBuffer.AppendInteger(threadId);
                messageBuffer.AppendInteger(group.Forum.ForumMessagesCount);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(Session.GetHabbo().Id);
                messageBuffer.AppendString(Session.GetHabbo().UserName);
                messageBuffer.AppendString(Session.GetHabbo().Look);
                messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - timestamp);
                messageBuffer.AppendString(content);
                messageBuffer.AppendByte(0);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendString(string.Empty);
                messageBuffer.AppendInteger(0);
                Session.SendMessage(messageBuffer);
            }
        }

        /// <summary>
        ///     Updates the state of the thread.
        /// </summary>
        internal void UpdateForumThread()
        {
            uint groupId = Request.GetUInteger();
            uint threadId = Request.GetUInteger();
            bool pin = Request.GetBool();
            bool Lock = Request.GetBool();

            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery(
                    $"SELECT * FROM groups_forums_posts WHERE group_id = '{groupId}' AND id = '{threadId}' LIMIT 1;");
                DataRow row = dbClient.GetRow();

                Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

                if (row != null)
                {
                    if ((uint) row["poster_id"] == Session.GetHabbo().Id ||
                        theGroup.Admins.ContainsKey(Session.GetHabbo().Id))
                    {
                        dbClient.SetQuery(
                            $"UPDATE groups_forums_posts SET pinned = @pin , locked = @lock WHERE id = {threadId};");
                        dbClient.AddParameter("pin", pin ? "1" : "0");
                        dbClient.AddParameter("lock", Lock ? "1" : "0");
                        dbClient.RunQuery();
                    }
                }

                GroupForumPost thread = new GroupForumPost(row);

                if (thread.Pinned != pin)
                {
                    SimpleServerMessageBuffer notif = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));

                    notif.AppendString(pin ? "forums.thread.pinned" : "forums.thread.unpinned");
                    notif.AppendInteger(0);
                    Session.SendMessage(notif);
                }

                if (thread.Locked != Lock)
                {
                    SimpleServerMessageBuffer notif2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));

                    notif2.AppendString(Lock ? "forums.thread.locked" : "forums.thread.unlocked");
                    notif2.AppendInteger(0);
                    Session.SendMessage(notif2);
                }

                if (thread.ParentId != 0)
                    return;

                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GroupForumThreadUpdateMessageComposer"));
                messageBuffer.AppendInteger(groupId);
                messageBuffer.AppendInteger(thread.Id);
                messageBuffer.AppendInteger(thread.PosterId);
                messageBuffer.AppendString(thread.PosterName);
                messageBuffer.AppendString(thread.Subject);
                messageBuffer.AppendBool(pin);
                messageBuffer.AppendBool(Lock);
                messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
                messageBuffer.AppendInteger(thread.MessageCount + 1);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendString(string.Empty);
                messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
                messageBuffer.AppendByte(thread.Hidden ? 10 : 1);
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendString(thread.Hider);
                messageBuffer.AppendInteger(0);

                Session.SendMessage(messageBuffer);
            }
        }

        /// <summary>
        ///     Alters the state of the forum thread.
        /// </summary>
        internal void AlterForumThreadState()
        {
            uint groupId = Request.GetUInteger();
            uint threadId = Request.GetUInteger();
            int stateToSet = Request.GetInteger();

            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery(
                    $"SELECT * FROM groups_forums_posts WHERE group_id = '{groupId}' AND id = '{threadId}' LIMIT 1;");

                DataRow row = dbClient.GetRow();
                Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

                if (row != null)
                {
                    if ((uint) row["poster_id"] == Session.GetHabbo().Id ||
                        theGroup.Admins.ContainsKey(Session.GetHabbo().Id))
                    {
                        dbClient.SetQuery($"UPDATE groups_forums_posts SET hidden = @hid WHERE id = {threadId};");
                        dbClient.AddParameter("hid", stateToSet == 20 ? "1" : "0");
                        dbClient.RunQuery();
                    }
                }

                GroupForumPost thread = new GroupForumPost(row);
                SimpleServerMessageBuffer notif = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));

                notif.AppendString(stateToSet == 20 ? "forums.thread.hidden" : "forums.thread.restored");
                notif.AppendInteger(0);
                Session.SendMessage(notif);

                if (thread.ParentId != 0)
                    return;

                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GroupForumThreadUpdateMessageComposer"));
                messageBuffer.AppendInteger(groupId);
                messageBuffer.AppendInteger(thread.Id);
                messageBuffer.AppendInteger(thread.PosterId);
                messageBuffer.AppendString(thread.PosterName);
                messageBuffer.AppendString(thread.Subject);
                messageBuffer.AppendBool(thread.Pinned);
                messageBuffer.AppendBool(thread.Locked);
                messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
                messageBuffer.AppendInteger(thread.MessageCount + 1);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendString(string.Empty);
                messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
                messageBuffer.AppendByte(stateToSet);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendString(thread.Hider);
                messageBuffer.AppendInteger(0);

                Session.SendMessage(messageBuffer);
            }
        }

        /// <summary>
        ///     Reads the forum thread.
        /// </summary>
        internal void ReadForumThread()
        {
            uint groupId = Request.GetUInteger();
            uint threadId = Request.GetUInteger();
            int startIndex = Request.GetInteger();

            Request.GetInteger();

            Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (theGroup == null || theGroup.Forum.Id == 0)
                return;

            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery(
                    $"SELECT * FROM groups_forums_posts WHERE group_id = '{groupId}' AND parent_id = '{threadId}' OR id = '{threadId}' ORDER BY timestamp ASC;");

                DataTable table = dbClient.GetTable();

                if (table == null)
                    return;

                int b = table.Rows.Count <= 20 ? table.Rows.Count : 20;

                List<GroupForumPost> posts = new List<GroupForumPost>();

                int i = 1;

                while (i <= b)
                {
                    DataRow row = table.Rows[i - 1];

                    if (row == null)
                    {
                        b--;
                        continue;
                    }

                    GroupForumPost thread = new GroupForumPost(row);

                    if (thread.ParentId == 0 && thread.Hidden)
                        return;

                    posts.Add(thread);

                    i++;
                }

                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GroupForumReadThreadMessageComposer"));

                messageBuffer.AppendInteger(groupId);
                messageBuffer.AppendInteger(threadId);
                messageBuffer.AppendInteger(startIndex);
                messageBuffer.AppendInteger(b);

                int indx = 0;

                foreach (GroupForumPost post in posts)
                {
                    messageBuffer.AppendInteger(indx++ - 1);
                    messageBuffer.AppendInteger(indx - 1);
                    messageBuffer.AppendInteger(post.PosterId);
                    messageBuffer.AppendString(post.PosterName);
                    messageBuffer.AppendString(post.PosterLook);
                    messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - post.Timestamp);
                    messageBuffer.AppendString(post.PostContent);
                    messageBuffer.AppendByte(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendString(post.Hider);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                }

                Session.SendMessage(messageBuffer);
            }
        }

        /// <summary>
        ///     Gets the group forum thread root.
        /// </summary>
        internal void GetGroupForumThreadRoot()
        {
            uint groupId = Request.GetUInteger();

            int startIndex = Request.GetInteger();

            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery($"SELECT count(id) FROM groups_forums_posts WHERE group_id = '{groupId}' AND parent_id = 0");

                dbClient.GetInteger();

                dbClient.SetQuery($"SELECT * FROM groups_forums_posts WHERE group_id = '{groupId}' AND parent_id = 0 ORDER BY timestamp DESC, pinned DESC LIMIT {startIndex}, {TotalPerPage}");

                DataTable table = dbClient.GetTable();
                int threadCount = table.Rows.Count <= TotalPerPage ? table.Rows.Count : TotalPerPage;

                List<GroupForumPost> threads = (from DataRow row in table.Rows select new GroupForumPost(row)).ToList();

                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GroupForumThreadRootMessageComposer"));
                messageBuffer.AppendInteger(groupId);
                messageBuffer.AppendInteger(startIndex);
                messageBuffer.AppendInteger(threadCount);

                foreach (GroupForumPost thread in threads)
                {
                    messageBuffer.AppendInteger(thread.Id);
                    messageBuffer.AppendInteger(thread.PosterId);
                    messageBuffer.AppendString(thread.PosterName);
                    messageBuffer.AppendString(thread.Subject);
                    messageBuffer.AppendBool(thread.Pinned);
                    messageBuffer.AppendBool(thread.Locked);
                    messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
                    messageBuffer.AppendInteger(thread.MessageCount + 1);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendString(string.Empty);
                    messageBuffer.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
                    messageBuffer.AppendByte(thread.Hidden ? 10 : 1);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendString(thread.Hider);
                    messageBuffer.AppendInteger(0);
                }

                Session.SendMessage(messageBuffer);
            }
        }

        /// <summary>
        ///     Gets the group forum data.
        /// </summary>
        internal void GetGroupForumData()
        {
            uint groupId = Request.GetUInteger();

            Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (theGroup != null && theGroup.Forum.Id != 0)
                Session.SendMessage(theGroup.ForumDataMessage(Session.GetHabbo().Id));
        }

        /// <summary>
        ///     Gets the group forums.
        /// </summary>
        internal void GetGroupForums()
        {
            int selectType = Request.GetInteger();
            int startIndex = Request.GetInteger();

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GroupForumListingsMessageComposer"));
            messageBuffer.AppendInteger(selectType);
            List<Group> groupList = new List<Group>();

            switch (selectType)
            {
                case 0:
                case 1:
                    using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.SetQuery("SELECT count(id) FROM groups_forums_data WHERE forum_messages_count > 0");

                        int qtdForums = dbClient.GetInteger();

                        dbClient.SetQuery(
                            "SELECT group_id FROM groups_forums_data WHERE forum_messages_count > 0 ORDER BY forum_messages_count DESC LIMIT @startIndex, @totalPerPage;");

                        dbClient.AddParameter("startIndex", startIndex);
                        dbClient.AddParameter("totalPerPage", TotalPerPage);

                        DataTable table = dbClient.GetTable();

                        messageBuffer.AppendInteger(qtdForums == 0 ? 1 : qtdForums);
                        messageBuffer.AppendInteger(startIndex);

                        groupList.AddRange(from DataRow rowGroupData in table.Rows
                            select uint.Parse(rowGroupData["group_id"].ToString())
                            into groupId
                            select Yupi.GetGame().GetGroupManager().GetGroup(groupId));

                        messageBuffer.AppendInteger(table.Rows.Count);

                        foreach (Group theGroup in groupList)
                            theGroup.SerializeForumRoot(messageBuffer);

                        Session.SendMessage(messageBuffer);
                    }
                    break;

                case 2:
                    groupList.AddRange(
                        Session.GetHabbo()
                            .UserGroups.Select(groupUser => Yupi.GetGame().GetGroupManager().GetGroup(groupUser.GroupId))
                            .Where(aGroup => aGroup != null && aGroup.Forum.Id != 0));

                    messageBuffer.AppendInteger(groupList.Count == 0 ? 1 : groupList.Count);

                    groupList =
                        groupList.OrderByDescending(x => x.Forum.ForumMessagesCount).Skip(startIndex).Take(20).ToList();

                    messageBuffer.AppendInteger(startIndex);
                    messageBuffer.AppendInteger(groupList.Count);

                    foreach (Group theGroup in groupList)
                        theGroup.SerializeForumRoot(messageBuffer);

                    Session.SendMessage(messageBuffer);
                    break;

                default:
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendInteger(startIndex);
                    messageBuffer.AppendInteger(0);
                    Session.SendMessage(messageBuffer);
                    break;
            }
        }

        /// <summary>
        ///     Manages the group.
        /// </summary>
        internal void ManageGroup()
        {
            uint groupId = Request.GetUInteger();
            Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (theGroup == null)
                return;

            if (!theGroup.Admins.ContainsKey(Session.GetHabbo().Id) && theGroup.CreatorId != Session.GetHabbo().Id &&
                Session.GetHabbo().Rank < 7)
                return;

            Response.Init(PacketLibraryManager.OutgoingHandler("GroupDataEditMessageComposer"));
            Response.AppendInteger(0);
            Response.AppendBool(true);
            Response.AppendInteger(theGroup.Id);
            Response.AppendString(theGroup.Name);
            Response.AppendString(theGroup.Description);
            Response.AppendInteger(theGroup.RoomId);
            Response.AppendInteger(theGroup.Colour1);
            Response.AppendInteger(theGroup.Colour2);
            Response.AppendInteger(theGroup.State);
            Response.AppendInteger(theGroup.AdminOnlyDeco);
            Response.AppendBool(false);
            Response.AppendString(string.Empty);

            string[] array = theGroup.Badge.Replace("b", string.Empty).Split('s');

            Response.AppendInteger(5);

            int num = 5 - array.Length;

            int num2 = 0;
            string[] array2 = array;

            foreach (string text in array2)
            {
                Response.AppendInteger(text.Length >= 6
                    ? uint.Parse(text.Substring(0, 3))
                    : uint.Parse(text.Substring(0, 2)));
                Response.AppendInteger(text.Length >= 6
                    ? uint.Parse(text.Substring(3, 2))
                    : uint.Parse(text.Substring(2, 2)));

                if (text.Length < 5)
                    Response.AppendInteger(0);
                else if (text.Length >= 6)
                    Response.AppendInteger(uint.Parse(text.Substring(5, 1)));
                else
                    Response.AppendInteger(uint.Parse(text.Substring(4, 1)));
            }

            while (num2 != num)
            {
                Response.AppendInteger(0);
                Response.AppendInteger(0);
                Response.AppendInteger(0);
                num2++;
            }

            Response.AppendString(theGroup.Badge);
            Response.AppendInteger(theGroup.Members.Count);

            SendResponse();
        }

        /// <summary>
        ///     Updates the name of the group.
        /// </summary>
        internal void UpdateGroupName()
        {
            uint num = Request.GetUInteger();
            string text = Request.GetString();
            string text2 = Request.GetString();

            Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(num);

            if (theGroup?.CreatorId != Session.GetHabbo().Id)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"UPDATE groups_data SET group_name = @name, group_description = @desc WHERE id={num} LIMIT 1");
                queryReactor.AddParameter("name", text);
                queryReactor.AddParameter("desc", text2);

                queryReactor.RunQuery();
            }

            theGroup.Name = text;
            theGroup.Description = text2;

            Yupi.GetGame()
                .GetGroupManager()
                .SerializeGroupInfo(theGroup, Response, Session, Session.GetHabbo().CurrentRoom);
        }

        /// <summary>
        ///     Updates the group badge.
        /// </summary>
        internal void UpdateGroupBadge()
        {
            uint guildId = Request.GetUInteger();

            Group guild = Yupi.GetGame().GetGroupManager().GetGroup(guildId);

            if (guild != null)
            {
                Room room = Yupi.GetGame().GetRoomManager().GetRoom(guild.RoomId);

                if (room != null)
                {
                    Request.GetInteger();

                    int Base = Request.GetInteger();
                    int baseColor = Request.GetInteger();

                    Request.GetInteger();

                    List<int> guildStates = new List<int>();

                    for (int i = 0; i < 12; i++)
                        guildStates.Add(Request.GetInteger());

                    string badge = Yupi.GetGame().GetGroupManager().GenerateGuildImage(Base, baseColor, guildStates);

                    guild.Badge = badge;

                    Response.Init(PacketLibraryManager.OutgoingHandler("RoomGroupMessageComposer"));
                    Response.AppendInteger(room.LoadedGroups.Count);

                    foreach (KeyValuePair<uint, string> current2 in room.LoadedGroups)
                    {
                        Response.AppendInteger(current2.Key);
                        Response.AppendString(current2.Value);
                    }

                    room.SendMessage(Response);

                    Yupi.GetGame().GetGroupManager().SerializeGroupInfo(guild, Response, Session, room);

                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery(
                            $"UPDATE groups_data SET group_badge = @badge WHERE id = {guildId}");
                        queryReactor.AddParameter("badge", badge);
                        queryReactor.RunQuery();
                    }

                    if (Session.GetHabbo().CurrentRoom != null)
                    {
                        Session.GetHabbo().CurrentRoom.LoadedGroups[guildId] = guild.Badge;

                        Response.Init(PacketLibraryManager.OutgoingHandler("RoomGroupMessageComposer"));
                        Response.AppendInteger(Session.GetHabbo().CurrentRoom.LoadedGroups.Count);

                        foreach (KeyValuePair<uint, string> current in Session.GetHabbo().CurrentRoom.LoadedGroups)
                        {
                            Response.AppendInteger(current.Key);
                            Response.AppendString(current.Value);
                        }

                        Session.GetHabbo().CurrentRoom.SendMessage(Response);

                        Yupi.GetGame()
                            .GetGroupManager()
                            .SerializeGroupInfo(guild, Response, Session, Session.GetHabbo().CurrentRoom);
                    }
                }
            }
        }

        /// <summary>
        ///     Updates the group colours.
        /// </summary>
        internal void UpdateGroupColours()
        {
            uint groupId = Request.GetUInteger();
            int num = Request.GetInteger();
            int num2 = Request.GetInteger();

            Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (theGroup?.CreatorId != Session.GetHabbo().Id)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("UPDATE groups_data SET colour1= ", num, ", colour2=",
                    num2, " WHERE id=", theGroup.Id, " LIMIT 1"));

            theGroup.Colour1 = num;
            theGroup.Colour2 = num2;

            Yupi.GetGame()
                .GetGroupManager()
                .SerializeGroupInfo(theGroup, Response, Session, Session.GetHabbo().CurrentRoom);
        }

        /// <summary>
        ///     Updates the group settings.
        /// </summary>
        internal void UpdateGroupSettings()
        {
            uint groupId = Request.GetUInteger();
            uint num = Request.GetUInteger();
            uint num2 = Request.GetUInteger();

            Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (theGroup?.CreatorId != Session.GetHabbo().Id)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("UPDATE groups_data SET state ='", num,
                    "', admindeco='", num2, "' WHERE id =", theGroup.Id, " LIMIT 1"));

            theGroup.State = num;
            theGroup.AdminOnlyDeco = num2;

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(theGroup.RoomId);

            if (room != null)
            {
                foreach (RoomUser current in room.GetRoomUserManager().GetRoomUsers())
                {
                    if (room.RoomData.OwnerId != current.UserId && !theGroup.Admins.ContainsKey(current.UserId) &&
                        theGroup.Members.ContainsKey(current.UserId))
                    {
                        if (num2 == 1u)
                        {
                            current.RemoveStatus("flatctrl 1");
                            Response.Init(PacketLibraryManager.OutgoingHandler("RoomRightsLevelMessageComposer"));
                            Response.AppendInteger(0);
                            current.GetClient().SendMessage(GetResponse());
                        }
                        else
                        {
                            if (num2 == 0u && !current.Statusses.ContainsKey("flatctrl 1"))
                            {
                                current.AddStatus("flatctrl 1", string.Empty);
                                Response.Init(PacketLibraryManager.OutgoingHandler("RoomRightsLevelMessageComposer"));
                                Response.AppendInteger(1);
                                current.GetClient().SendMessage(GetResponse());
                            }
                        }

                        current.UpdateNeeded = true;
                    }
                }
            }

            Yupi.GetGame()
                .GetGroupManager()
                .SerializeGroupInfo(theGroup, Response, Session, Session.GetHabbo().CurrentRoom);
        }

        /// <summary>
        ///     Requests the leave group.
        /// </summary>
        internal void RequestLeaveGroup()
        {
            uint groupId = Request.GetUInteger();
            uint userId = Request.GetUInteger();

            Group guild = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (guild == null || guild.CreatorId == userId)
                return;

            if (userId == Session.GetHabbo().Id || guild.Admins.ContainsKey(Session.GetHabbo().Id))
            {
                Response.Init(PacketLibraryManager.OutgoingHandler("GroupAreYouSureMessageComposer"));
                Response.AppendInteger(userId);
                Response.AppendInteger(0);
                SendResponse();
            }
        }

        /// <summary>
        ///     Confirms the leave group.
        /// </summary>
        internal void ConfirmLeaveGroup()
        {
            uint guild = Request.GetUInteger();

            uint userId = Request.GetUInteger();

            Group byeGuild = Yupi.GetGame().GetGroupManager().GetGroup(guild);

            if (byeGuild == null)
                return;

            if (byeGuild.CreatorId == userId)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("user_room_video_true"));

                return;
            }

            if (userId == Session.GetHabbo().Id || byeGuild.Admins.ContainsKey(Session.GetHabbo().Id))
            {
                GroupMember memberShip;

                int type = 3;

                if (!byeGuild.Members.ContainsKey(userId) && !byeGuild.Admins.ContainsKey(userId))
                    return;

                if (byeGuild.Members.ContainsKey(userId))
                {
                    memberShip = byeGuild.Members[userId];

                    type = 3;

                    Session.GetHabbo().UserGroups.Remove(memberShip);
                    byeGuild.Members.Remove(userId);
                }

                if (byeGuild.Admins.ContainsKey(userId))
                {
                    memberShip = byeGuild.Admins[userId];

                    type = 1;

                    Session.GetHabbo().UserGroups.Remove(memberShip);
                    byeGuild.Admins.Remove(userId);
                }   

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery(string.Concat("DELETE FROM groups_members WHERE user_id=", userId, " AND group_id=", guild, " LIMIT 1"));

                Habbo byeUser = Yupi.GetHabboById(userId);

                if (byeUser != null)
                {
                    Response.Init(PacketLibraryManager.OutgoingHandler("GroupConfirmLeaveMessageComposer"));
                    Response.AppendInteger(guild);
                    Response.AppendInteger(type);
                    Response.AppendInteger(byeUser.Id);
                    Response.AppendString(byeUser.UserName);
                    Response.AppendString(byeUser.Look);
                    Response.AppendString(string.Empty);

                    SendResponse();
                }

                if (byeUser != null && byeUser.FavouriteGroup == guild)
                {
                    byeUser.FavouriteGroup = 0;

                    using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                        queryreactor2.RunFastQuery($"UPDATE users_stats SET favourite_group = 0 WHERE id = {userId} LIMIT 1");

                    Room room = Session.GetHabbo().CurrentRoom;

                    Response.Init(PacketLibraryManager.OutgoingHandler("FavouriteGroupMessageComposer"));
                    Response.AppendInteger(byeUser.Id);

                    if (room != null)
                        room.SendMessage(Response);
                    else
                        SendResponse();

                    Response.Init(PacketLibraryManager.OutgoingHandler("ChangeFavouriteGroupMessageComposer"));
                    Response.AppendInteger(0);
                    Response.AppendInteger(-1);
                    Response.AppendInteger(-1);
                    Response.AppendString(string.Empty);

                    room?.SendMessage(Response);
                }

                if (byeGuild.AdminOnlyDeco == 0u)
                {
                    if (Yupi.GetGame().GetRoomManager().GetRoom(byeGuild.RoomId).GetRoomUserManager().GetRoomUserByHabbo(byeUser?.UserName) != null)
                    {
                        Response.Init(PacketLibraryManager.OutgoingHandler("RoomRightsLevelMessageComposer"));

                        Response.AppendInteger(0);

                        SendResponse();
                    }
                }

                Yupi.GetGame().GetGroupManager().SerializeGroupInfo(byeGuild, Response, Session);

                Response.Init(PacketLibraryManager.OutgoingHandler("GroupMembersMessageComposer"));

                Yupi.GetGame().GetGroupManager().SerializeGroupMembers(Response, byeGuild, 0u, Session);

                SendResponse();

                Response.Init(PacketLibraryManager.OutgoingHandler("GroupRequestReloadMessageComposer"));
                Response.AppendInteger(guild);

                SendResponse();
            }
        }

        internal void UpdateForumSettings()
        {
            uint guild = Request.GetUInteger();
            uint whoCanRead = Request.GetUInteger();
            uint whoCanPost = Request.GetUInteger();
            uint whoCanThread = Request.GetUInteger();
            uint whoCanMod = Request.GetUInteger();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(guild);

            if (group == null)
                return;

            group.Forum.WhoCanRead = whoCanRead;
            group.Forum.WhoCanPost = whoCanPost;
            group.Forum.WhoCanThread = whoCanThread;
            group.Forum.WhoCanMod = whoCanMod;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    "UPDATE groups_forums_data SET who_can_read = @who_can_read, who_can_post = @who_can_post, who_can_thread = @who_can_thread, who_can_mod = @who_can_mod WHERE group_id = @group_id");
                queryReactor.AddParameter("group_id", group.Id);
                queryReactor.AddParameter("who_can_read", whoCanRead);
                queryReactor.AddParameter("who_can_post", whoCanPost);
                queryReactor.AddParameter("who_can_thread", whoCanThread);
                queryReactor.AddParameter("who_can_mod", whoCanMod);
                queryReactor.RunQuery();
            }

            Session.SendMessage(group.ForumDataMessage(Session.GetHabbo().Id));
        }

        internal void DeleteGroup()
        {
            uint groupId = Request.GetUInteger();

            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

            if (room?.RoomData?.Group == null)
                Session.SendNotif(Yupi.GetLanguage().GetVar("command_group_has_no_room"));
            else
            {
                foreach (GroupMember user in group.Members.Values)
                {
                    GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(user.Id);

                    if (clientByUserId == null)
                        continue;

                    clientByUserId.GetHabbo().UserGroups.Remove(user);

                    if (clientByUserId.GetHabbo().FavouriteGroup == group.Id)
                        clientByUserId.GetHabbo().FavouriteGroup = 0;
                }

                room.RoomData.Group = null;
                room.RoomData.GroupId = 0;

                Yupi.GetGame().GetGroupManager().DeleteGroup(@group.Id);

                SimpleServerMessageBuffer deleteGroup = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GroupDeletedMessageComposer"));

                deleteGroup.AppendInteger(groupId);
                room.SendMessage(deleteGroup);

                List<RoomItem> roomItemList = room.GetRoomItemHandler().RemoveAllFurniture(Session);
                room.GetRoomItemHandler().RemoveItemsByOwner(ref roomItemList, ref Session);
                RoomData roomData = room.RoomData;
                uint roomId = room.RoomData.Id;

                Yupi.GetGame().GetRoomManager().UnloadRoom(room, "Delete room");
                Yupi.GetGame().GetRoomManager().QueueVoteRemove(roomData);

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.RunFastQuery($"DELETE FROM rooms_data WHERE id = {roomId}");
                    queryReactor.RunFastQuery($"DELETE FROM users_favorites WHERE room_id = {roomId}");
                    queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE room_id = {roomId}");
                    queryReactor.RunFastQuery($"DELETE FROM rooms_rights WHERE room_id = {roomId}");
                    queryReactor.RunFastQuery($"UPDATE users SET home_room = '0' WHERE home_room = {roomId}");
                }

                RoomData roomData2 =
                    (from p in Session.GetHabbo().UsersRooms where p.Id == roomId select p).SingleOrDefault();

                if (roomData2 != null)
                    Session.GetHabbo().UsersRooms.Remove(roomData2);
            }
        }
    }
}