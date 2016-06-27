using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Yupi.Emulator.Core.Algorithms.Encryption;
using Yupi.Emulator.Core.Io;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Security.BlackWords;
using Yupi.Emulator.Core.Security.BlackWords.Enums;
using Yupi.Emulator.Core.Security.BlackWords.Structs;
using Yupi.Emulator.Core.Settings;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Browser;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.Catalogs;
using Yupi.Emulator.Game.Catalogs.Composers;
using Yupi.Emulator.Game.Catalogs.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Pathfinding;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.Pets.Enums;
using Yupi.Emulator.Game.Pets.Structs;
using Yupi.Emulator.Game.Polls;
using Yupi.Emulator.Game.Polls.Enums;
using Yupi.Emulator.Game.RoomBots;
using Yupi.Emulator.Game.RoomBots.Enumerators;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Competitions.Models;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Net.Web;

namespace Yupi.Emulator.Messages.Handlers
{
     public partial class MessageHandler
    {
        private int _floodCount;

        private DateTime _floodTime;

     public void OnRoomUserAdd()
        {
            if (Session == null || GetResponse() == null)
                return;

            QueuedServerMessageBuffer queuedServerMessageBuffer = new QueuedServerMessageBuffer(Session.GetConnection());

            if (CurrentLoadingRoom?.GetRoomUserManager() == null || CurrentLoadingRoom.GetRoomUserManager().UserList == null)
                return;

            IEnumerable<RoomUser> list = CurrentLoadingRoom.GetRoomUserManager().UserList.Values.Where(current => current != null && !current.IsSpectator);

            Response.Init(PacketLibraryManager.OutgoingHandler("SetRoomUserMessageComposer"));
            Response.StartArray();

            foreach (RoomUser current2 in list)
            {
                try
                {
                    current2.Serialize(Response, CurrentLoadingRoom.GetGameMap().GotPublicPool);
                    Response.SaveArray();
                }
                catch (Exception e)
                {
                    YupiLogManager.LogException(e, "Registered Room YupiDatabaseManager Exception.");
                    Response.Clear();
                }
            }

            Response.EndArray();

            queuedServerMessageBuffer.AppendResponse(GetResponse());
            queuedServerMessageBuffer.AppendResponse(RoomFloorAndWallComposer(CurrentLoadingRoom));
            queuedServerMessageBuffer.AppendResponse(GetResponse());

            Response.Init(PacketLibraryManager.OutgoingHandler("RoomOwnershipMessageComposer"));
            Response.AppendInteger(CurrentLoadingRoom.RoomId);
            Response.AppendBool(CurrentLoadingRoom.CheckRights(Session, true));
            queuedServerMessageBuffer.AppendResponse(GetResponse());

            foreach (Habbo habboForId in CurrentLoadingRoom.UsersWithRights.Select(Yupi.GetHabboById))
            {
                if (habboForId == null) continue;

                GetResponse().Init(PacketLibraryManager.OutgoingHandler("GiveRoomRightsMessageComposer"));
                GetResponse().AppendInteger(CurrentLoadingRoom.RoomId);
                GetResponse().AppendInteger(habboForId.Id);
                GetResponse().AppendString(habboForId.UserName);
                queuedServerMessageBuffer.AppendResponse(GetResponse());
            }

            SimpleServerMessageBuffer simpleServerMessageBuffer = CurrentLoadingRoom.GetRoomUserManager().SerializeStatusUpdates(true);

            if (simpleServerMessageBuffer != null)
                queuedServerMessageBuffer.AppendResponse(simpleServerMessageBuffer);

            if (CurrentLoadingRoom.RoomData.Event != null)
                Yupi.GetGame().GetRoomEvents().SerializeEventInfo(CurrentLoadingRoom.RoomId);

            CurrentLoadingRoom.JustLoaded = false;

            foreach (RoomUser current4 in CurrentLoadingRoom.GetRoomUserManager().UserList.Values.Where(current4 => current4 != null))
            {
                if (current4.IsBot)
                {
                    if (current4.BotData.DanceId > 0)
                    {
                        Response.Init(PacketLibraryManager.OutgoingHandler("DanceStatusMessageComposer"));
                        Response.AppendInteger(current4.VirtualId);
                        Response.AppendInteger(current4.BotData.DanceId);
                        queuedServerMessageBuffer.AppendResponse(GetResponse());
                    }
                }
                else if (current4.IsDancing)
                {
                    Response.Init(PacketLibraryManager.OutgoingHandler("DanceStatusMessageComposer"));
                    Response.AppendInteger(current4.VirtualId);
                    Response.AppendInteger(current4.DanceId);
                    queuedServerMessageBuffer.AppendResponse(GetResponse());
                }

                if (current4.IsAsleep)
                {
                    SimpleServerMessageBuffer sleepMsg = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomUserIdleMessageComposer"));
                    sleepMsg.AppendInteger(current4.VirtualId);
                    sleepMsg.AppendBool(true);
                    queuedServerMessageBuffer.AppendResponse(sleepMsg);
                }

                if (current4.CarryItemId > 0 && current4.CarryTimer > 0)
                {
                    Response.Init(PacketLibraryManager.OutgoingHandler("ApplyHanditemMessageComposer"));
                    Response.AppendInteger(current4.VirtualId);
                    Response.AppendInteger(current4.CarryTimer);
                    queuedServerMessageBuffer.AppendResponse(GetResponse());
                }

                if (current4.IsBot)
                    continue;

                try
                {
                    if (current4.GetClient() != null && current4.GetClient().GetHabbo() != null)
                    {
                        if (current4.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent() != null && current4.CurrentEffect >= 1)
                        {
                            Response.Init(PacketLibraryManager.OutgoingHandler("ApplyEffectMessageComposer"));
                            Response.AppendInteger(current4.VirtualId);
                            Response.AppendInteger(current4.CurrentEffect);
                            Response.AppendInteger(0);
                            queuedServerMessageBuffer.AppendResponse(GetResponse());
                        }

                        SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateUserDataMessageComposer"));
                        simpleServerMessage2.AppendInteger(current4.VirtualId);
                        simpleServerMessage2.AppendString(current4.GetClient().GetHabbo().Look);
                        simpleServerMessage2.AppendString(current4.GetClient().GetHabbo().Gender.ToLower());
                        simpleServerMessage2.AppendString(current4.GetClient().GetHabbo().Motto);
                        simpleServerMessage2.AppendInteger(current4.GetClient().GetHabbo().AchievementPoints);
                        CurrentLoadingRoom?.SendMessage(simpleServerMessage2);
                    }
                }
                catch (Exception e)
                {
                    YupiLogManager.LogException(e, "Failed Broadcasting Room Data to Client.", "Yupi.Room");
                }
            }

            queuedServerMessageBuffer.SendResponse();
        }

     public void PrepareRoomForUser(uint id, string pWd, bool isReload = false)
        {
            try
            {
                if (Session?.GetHabbo() == null || Session.GetHabbo().LoadingRoom == id)
                    return;

                if (Yupi.ShutdownStarted)
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("server_shutdown"));
                    return;
                }

                Session.GetHabbo().LoadingRoom = id;

                QueuedServerMessageBuffer queuedServerMessageBuffer = new QueuedServerMessageBuffer(Session.GetConnection());

                Room room;

                if (Session.GetHabbo().InRoom)
                {
                    room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

                    if (room?.GetRoomUserManager() != null)
                        room.GetRoomUserManager().RemoveUserFromRoom(Session, false, false);
                }

                room = Yupi.GetGame().GetRoomManager().LoadRoom(id);

                if (room == null)
                    return;

                if (room.UserCount >= room.RoomData.UsersMax && !Session.GetHabbo().HasFuse("fuse_enter_full_rooms") &&
                    Session.GetHabbo().Id != (ulong) room.RoomData.OwnerId)
                {
                    SimpleServerMessageBuffer roomQueue = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomsQueue"));

                    roomQueue.AppendInteger(2);
                    roomQueue.AppendString("visitors");
                    roomQueue.AppendInteger(2);
                    roomQueue.AppendInteger(1);
                    roomQueue.AppendString("visitors");
                    roomQueue.AppendInteger(room.UserCount - (int) room.RoomData.UsersNow);
                    roomQueue.AppendString("spectators");
                    roomQueue.AppendInteger(1);
                    roomQueue.AppendInteger(1);
                    roomQueue.AppendString("spectators");
                    roomQueue.AppendInteger(0);

                    Session.SendMessage(roomQueue);

                    //ClearRoomLoading();
                    return;
                }

                CurrentLoadingRoom = room;

                if (!Session.GetHabbo().HasFuse("fuse_enter_any_room") && room.UserIsBanned(Session.GetHabbo().Id))
                {
                    if (!room.HasBanExpired(Session.GetHabbo().Id))
                    {
                        ClearRoomLoading();

                        SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomEnterErrorMessageComposer"));
                        simpleServerMessage2.AppendInteger(4);
                        Session.SendMessage(simpleServerMessage2);
                        Response.Init(PacketLibraryManager.OutgoingHandler("OutOfRoomMessageComposer"));

                        queuedServerMessageBuffer.AppendResponse(GetResponse());
                        queuedServerMessageBuffer.SendResponse();

                        return;
                    }

                    room.RemoveBan(Session.GetHabbo().Id);
                }

                Response.Init(PacketLibraryManager.OutgoingHandler("PrepareRoomMessageComposer"));

                queuedServerMessageBuffer.AppendResponse(GetResponse());

                if (!isReload && !Session.GetHabbo().HasFuse("fuse_enter_any_room") && !room.CheckRightsDoorBell(Session, true, true, room.RoomData.Group != null && room.RoomData.Group.Members.ContainsKey(Session.GetHabbo().Id)) && !(Session.GetHabbo().IsTeleporting && Session.GetHabbo().TeleportingRoomId == id) && !Session.GetHabbo().IsHopping)
                {
                    if (room.RoomData.State == 1)
                    {
                        if (room.UserCount == 0)
                        {
                            Response.Init(PacketLibraryManager.OutgoingHandler("DoorbellNoOneMessageComposer"));

                            queuedServerMessageBuffer.AppendResponse(GetResponse());
                        }
                        else
                        {
                            Response.Init(PacketLibraryManager.OutgoingHandler("DoorbellMessageComposer"));
                            Response.AppendString(string.Empty);

                            queuedServerMessageBuffer.AppendResponse(GetResponse());

                            SimpleServerMessageBuffer simpleServerMessage3 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("DoorbellMessageComposer"));
                            simpleServerMessage3.AppendString(Session.GetHabbo().UserName);

                            room.SendMessageToUsersWithRights(simpleServerMessage3);
                        }

                        queuedServerMessageBuffer.SendResponse();

                        return;
                    }

                    if (room.RoomData.State == 2 && !string.Equals(pWd, room.RoomData.PassWord, StringComparison.CurrentCultureIgnoreCase))
                    {
                        ClearRoomLoading();

                        Session.GetMessageHandler()
                            .GetResponse()
                            .Init(PacketLibraryManager.OutgoingHandler("RoomErrorMessageComposer"));
                        Session.GetMessageHandler().GetResponse().AppendInteger(-100002);
                        Session.GetMessageHandler().SendResponse();

                        Session.GetMessageHandler()
                            .GetResponse()
                            .Init(PacketLibraryManager.OutgoingHandler("OutOfRoomMessageComposer"));
                        Session.GetMessageHandler().GetResponse();
                        Session.GetMessageHandler().SendResponse();

                        return;
                    }
                }

                Session.GetHabbo().LoadingChecksPassed = true;

                queuedServerMessageBuffer.AddBytes(LoadRoomForUser().GetPacket);

                queuedServerMessageBuffer.SendResponse();

                if (Session.GetHabbo().RecentlyVisitedRooms.Contains(room.RoomId))
                    Session.GetHabbo().RecentlyVisitedRooms.Remove(room.RoomId);

                Session.GetHabbo().RecentlyVisitedRooms.AddFirst(room.RoomId);
            }
            catch (Exception e)
            {
                YupiLogManager.LogException("PrepareRoomForUser. RoomId: " + id + "; UserId: " + (Session?.GetHabbo().Id.ToString(CultureInfo.InvariantCulture) ?? "null") + Environment.NewLine + e, "Failed Preparing Room for User.");
            }
        }
			

     public void ClearRoomLoading()
        {
            if (Session?.GetHabbo() == null)
                return;

            Session.GetHabbo().LoadingRoom = 0u;
            Session.GetHabbo().LoadingChecksPassed = false;
        }



     public static SimpleServerMessageBuffer RoomFloorAndWallComposer(Room room)
		{
			SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer (PacketLibraryManager.OutgoingHandler ("RoomFloorWallLevelsMessageComposer"));
			simpleServerMessageBuffer.AppendBool (room.RoomData.HideWall);
			simpleServerMessageBuffer.AppendInteger (room.RoomData.WallThickness);
			simpleServerMessageBuffer.AppendInteger (room.RoomData.FloorThickness);
			return simpleServerMessageBuffer;
		}
    }
}