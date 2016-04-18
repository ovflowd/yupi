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

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class MessageHandler.
    /// </summary>
     public partial class MessageHandler
    {
        /// <summary>
        ///     Manages the group.
        /// </summary>
     public void ManageGroup()
        {
            uint groupId = Request.GetUInt32();
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
        ///     Updates the group badge.
        /// </summary>
     public void UpdateGroupBadge()
        {
            uint guildId = Request.GetUInt32();

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
     public void UpdateGroupColours()
        {
            uint groupId = Request.GetUInt32();
            int num = Request.GetInteger();
            int num2 = Request.GetInteger();

            Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (theGroup?.CreatorId != Session.GetHabbo().Id)
                return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				// TODO Fix Colour to Color
				queryReactor.SetQuery("UPDATE groups_data Set colour1 = @color1, colour2 = @color2 WHERE id = @id");
				queryReactor.AddParameter("color1", num);
				queryReactor.AddParameter("color2", num2);
				queryReactor.AddParameter("id", theGroup.Id);
				queryReactor.RunQuery ();
			}
			// TODO Refactor assign numbers earlier and implement save method on group!
            theGroup.Colour1 = num;
            theGroup.Colour2 = num2;

            Yupi.GetGame()
                .GetGroupManager()
                .SerializeGroupInfo(theGroup, Response, Session, Session.GetHabbo().CurrentRoom);
        }

        /// <summary>
        ///     Updates the group settings.
        /// </summary>
     public void UpdateGroupSettings()
        {
            uint groupId = Request.GetUInt32();
            uint num = Request.GetUInt32();
            uint num2 = Request.GetUInt32();

            Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (theGroup?.CreatorId != Session.GetHabbo().Id)
                return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("UPDATE groups_data SET state = @state, admindeco = @admindeco WHERE id = @id");
				queryReactor.AddParameter("state", num);
				queryReactor.AddParameter("admindeco", num2);
				queryReactor.AddParameter("id", theGroup.Id);
				queryReactor.RunQuery ();
			}

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
     public void RequestLeaveGroup()
        {
            uint groupId = Request.GetUInt32();
            uint userId = Request.GetUInt32();

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
     public void ConfirmLeaveGroup()
        {
            uint guild = Request.GetUInt32();

            uint userId = Request.GetUInt32();

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

				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
					queryReactor.SetQuery ("DELETE FROM groups_members WHERE user_id = @user_id AND group_id = @group_id");
					queryReactor.AddParameter ("user_id", userId);
					queryReactor.AddParameter ("group_id", guild);
					queryReactor.RunQuery ();
				}

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

     public void UpdateForumSettings()
        {
            uint guild = Request.GetUInt32();
            uint whoCanRead = Request.GetUInt32();
            uint whoCanPost = Request.GetUInt32();
            uint whoCanThread = Request.GetUInt32();
            uint whoCanMod = Request.GetUInt32();

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

     public void DeleteGroup()
        {
            uint groupId = Request.GetUInt32();

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
					// TODO Use parameters (or ORM)
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