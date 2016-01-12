using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Yupi.Core.Encryption.Utils;
using Yupi.Core.Io;
using Yupi.Core.Security.BlackWords;
using Yupi.Core.Security.BlackWords.Enums;
using Yupi.Core.Security.BlackWords.Structs;
using Yupi.Core.Settings;
using Yupi.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Browser;
using Yupi.Game.Browser.Models;
using Yupi.Game.Catalogs;
using Yupi.Game.Catalogs.Composers;
using Yupi.Game.Catalogs.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Groups.Structs;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pathfinding;
using Yupi.Game.Pets;
using Yupi.Game.Pets.Enums;
using Yupi.Game.Pets.Structs;
using Yupi.Game.Polls;
using Yupi.Game.Polls.Enums;
using Yupi.Game.RoomBots;
using Yupi.Game.RoomBots.Enumerators;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Rooms.User;
using Yupi.Game.Users;
using Yupi.Messages.Parsers;
using Yupi.Net.Web;

namespace Yupi.Messages.Handlers
{
    internal partial class GameClientMessageHandler
    {
        private int _floodCount;
        private DateTime _floodTime;

        public void GetPetBreeds()
        {
            string type = Request.GetString();

            string petType = PetTypeManager.GetPetTypeByHabboPetType(type);

            uint petId = PetTypeManager.GetPetRaceByItemName(petType);

            List<PetRace> races = PetTypeManager.GetRacesByPetType(petType);

            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("SellablePetBreedsMessageComposer"));

            message.AppendString(type);
            message.AppendInteger(races.Count);

            foreach (PetRace current in races)
            {
                message.AppendInteger(petId);
                message.AppendInteger(current.ColorOne);
                message.AppendInteger(current.ColorTwo);
                message.AppendBool(current.HasColorOne);
                message.AppendBool(current.HasColorTwo);
            }

            Session.SendMessage(message);
        }

        internal void GoRoom()
        {
            if (Yupi.ShutdownStarted || Session?.GetHabbo() == null)
                return;
            uint num = Request.GetUInteger();
            RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(num);
            Session.GetHabbo().GetInventoryComponent().RunDbUpdate();
            if (roomData == null || roomData.Id == Session.GetHabbo().CurrentRoomId)
                return;
            roomData.SerializeRoomData(Response, Session, !Session.GetHabbo().InRoom);
            PrepareRoomForUser(num, roomData.PassWord);
        }

        internal void AddFavorite()
        {
            if (Session.GetHabbo() == null)
                return;

            uint roomId = Request.GetUInteger();

            GetResponse().Init(LibraryParser.OutgoingRequest("FavouriteRoomsUpdateMessageComposer"));
            GetResponse().AppendInteger(roomId);
            GetResponse().AppendBool(true);
            SendResponse();

            Session.GetHabbo().FavoriteRooms.Add(roomId);
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery("INSERT INTO users_favorites (user_id,room_id) VALUES (" +
                                                    Session.GetHabbo().Id + "," + roomId + ")");
        }

        internal void RemoveFavorite()
        {
            if (Session.GetHabbo() == null)
                return;
            uint roomId = Request.GetUInteger();
            Session.GetHabbo().FavoriteRooms.Remove(roomId);

            GetResponse().Init(LibraryParser.OutgoingRequest("FavouriteRoomsUpdateMessageComposer"));
            GetResponse().AppendInteger(roomId);
            GetResponse().AppendBool(false);
            SendResponse();

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery("DELETE FROM users_favorites WHERE user_id = " +
                                                    Session.GetHabbo().Id + " AND room_id = " + roomId);
        }

        internal void OnlineConfirmationEvent()
        {
            Writer.WriteLine(
                Request.GetString() + " connected in the game. with ip " + Session.GetConnection().GetIp(), "Yupi.Users",
                ConsoleColor.DarkGreen);

            if (!ServerConfigurationSettings.Data.ContainsKey("welcome.message.enabled") ||
                ServerConfigurationSettings.Data["welcome.message.enabled"] != "true")
                return;

            if (!ServerConfigurationSettings.Data.ContainsKey("welcome.message.image") ||
                string.IsNullOrEmpty(ServerConfigurationSettings.Data["welcome.message.image"]))
                Session.SendNotifWithScroll(ServerExtraSettings.WelcomeMessage.Replace("%username%",
                    Session.GetHabbo().UserName));
            else
                Session.SendNotif(ServerExtraSettings.WelcomeMessage.Replace("%username%", Session.GetHabbo().UserName),
                    ServerConfigurationSettings.Data.ContainsKey("welcome.message.title")
                        ? ServerConfigurationSettings.Data["welcome.message.title"]
                        : string.Empty, ServerConfigurationSettings.Data["welcome.message.image"]);
        }

        internal void GoToHotelView()
        {
            if (Session == null || Session.GetHabbo() == null)
                return;
            if (!Session.GetHabbo().InRoom)
                return;
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room != null)
                room.GetRoomUserManager().RemoveUserFromRoom(Session, true, false);

            HotelLandingManager hotelView = Yupi.GetGame().GetHotelView();
            if (hotelView.FurniRewardName != null)
            {
                ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("LandingRewardMessageComposer"));
                serverMessage.AppendString(hotelView.FurniRewardName);
                serverMessage.AppendInteger(hotelView.FurniRewardId);
                serverMessage.AppendInteger(120);
                serverMessage.AppendInteger(120 - Session.GetHabbo().Respect);
                Session.SendMessage(serverMessage);
            }
            Session.CurrentRoomUserId = -1;
        }

        internal void LandingCommunityGoal()
        {
            int onlineFriends = Session.GetHabbo().GetMessenger().Friends.Count(x => x.Value.IsOnline);
            ServerMessage goalMeter = new ServerMessage(LibraryParser.OutgoingRequest("LandingCommunityChallengeMessageComposer"));
            goalMeter.AppendBool(true); //
            goalMeter.AppendInteger(0); //points
            goalMeter.AppendInteger(0); //my rank
            goalMeter.AppendInteger(onlineFriends); //totalAmount
            goalMeter.AppendInteger(onlineFriends >= 20 ? 1 : onlineFriends >= 50 ? 2 : onlineFriends >= 80 ? 3 : 0);
            //communityHighestAchievedLevel
            goalMeter.AppendInteger(0); //scoreRemainingUntilNextLevel
            goalMeter.AppendInteger(0); //percentCompletionTowardsNextLevel
            goalMeter.AppendString("friendshipChallenge"); //Type
            goalMeter.AppendInteger(0); //unknown
            goalMeter.AppendInteger(0); //ranks and loop
            Session.SendMessage(goalMeter);
        }

        internal void RequestFloorItems()
        {
        }

        internal void RequestWallItems()
        {
        }

        internal void SaveBranding()
        {
            uint itemId = Request.GetUInteger();
            uint count = Request.GetUInteger();

            if (Session == null || Session.GetHabbo() == null) return;
            Room room = Session.GetHabbo().CurrentRoom;
            if (room == null || !room.CheckRights(Session, true)) return;

            RoomItem item = room.GetRoomItemHandler().GetItem(itemId);
            if (item == null)
                return;

            string extraData = $"state{Convert.ToChar(9)}0";
            for (uint i = 1; i <= count; i++)
                extraData = $"{extraData}{Convert.ToChar(9)}{Request.GetString()}";

            item.ExtraData = extraData;
            room.GetRoomItemHandler()
                .SetFloorItem(Session, item, item.X, item.Y, item.Rot, false, false, true);
        }

        internal void OnRoomUserAdd()
        {
            if (Session == null || GetResponse() == null)
                return;
            QueuedServerMessage queuedServerMessage = new QueuedServerMessage(Session.GetConnection());
            if (CurrentLoadingRoom == null || CurrentLoadingRoom.GetRoomUserManager() == null ||
                CurrentLoadingRoom.GetRoomUserManager().UserList == null)
                return;
            IEnumerable<RoomUser> list =
                CurrentLoadingRoom.GetRoomUserManager()
                    .UserList.Values.Where(current => current != null && !current.IsSpectator);
            Response.Init(LibraryParser.OutgoingRequest("SetRoomUserMessageComposer"));
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
                    ServerLogManager.LogException(e.ToString());
                    Response.Clear();
                }
            }
            Response.EndArray();

            queuedServerMessage.AppendResponse(GetResponse());
            queuedServerMessage.AppendResponse(RoomFloorAndWallComposer(CurrentLoadingRoom));
            queuedServerMessage.AppendResponse(GetResponse());

            Response.Init(LibraryParser.OutgoingRequest("RoomOwnershipMessageComposer"));
            Response.AppendInteger(CurrentLoadingRoom.RoomId);
            Response.AppendBool(CurrentLoadingRoom.CheckRights(Session, true));
            queuedServerMessage.AppendResponse(GetResponse());

            foreach (Habbo habboForId in CurrentLoadingRoom.UsersWithRights.Select(Yupi.GetHabboById))
            {
                if (habboForId == null) continue;

                GetResponse().Init(LibraryParser.OutgoingRequest("GiveRoomRightsMessageComposer"));
                GetResponse().AppendInteger(CurrentLoadingRoom.RoomId);
                GetResponse().AppendInteger(habboForId.Id);
                GetResponse().AppendString(habboForId.UserName);
                queuedServerMessage.AppendResponse(GetResponse());
            }

            ServerMessage serverMessage = CurrentLoadingRoom.GetRoomUserManager().SerializeStatusUpdates(true);
            if (serverMessage != null)
                queuedServerMessage.AppendResponse(serverMessage);

            if (CurrentLoadingRoom.RoomData.Event != null)
                Yupi.GetGame().GetRoomEvents().SerializeEventInfo(CurrentLoadingRoom.RoomId);

            CurrentLoadingRoom.JustLoaded = false;
            foreach (
                RoomUser current4 in
                    CurrentLoadingRoom.GetRoomUserManager().UserList.Values.Where(current4 => current4 != null))
            {
                if (current4.IsBot)
                {
                    if (current4.BotData.DanceId > 0)
                    {
                        Response.Init(LibraryParser.OutgoingRequest("DanceStatusMessageComposer"));
                        Response.AppendInteger(current4.VirtualId);
                        Response.AppendInteger(current4.BotData.DanceId);
                        queuedServerMessage.AppendResponse(GetResponse());
                    }
                }
                else if (current4.IsDancing)
                {
                    Response.Init(LibraryParser.OutgoingRequest("DanceStatusMessageComposer"));
                    Response.AppendInteger(current4.VirtualId);
                    Response.AppendInteger(current4.DanceId);
                    queuedServerMessage.AppendResponse(GetResponse());
                }
                if (current4.IsAsleep)
                {
                    ServerMessage sleepMsg = new ServerMessage(LibraryParser.OutgoingRequest("RoomUserIdleMessageComposer"));
                    sleepMsg.AppendInteger(current4.VirtualId);
                    sleepMsg.AppendBool(true);
                    queuedServerMessage.AppendResponse(sleepMsg);
                }
                if (current4.CarryItemId > 0 && current4.CarryTimer > 0)
                {
                    Response.Init(LibraryParser.OutgoingRequest("ApplyHanditemMessageComposer"));
                    Response.AppendInteger(current4.VirtualId);
                    Response.AppendInteger(current4.CarryTimer);
                    queuedServerMessage.AppendResponse(GetResponse());
                }
                if (current4.IsBot)
                    continue;
                try
                {
                    if (current4.GetClient() != null &&
                        current4.GetClient().GetHabbo() != null)
                    {
                        if (current4.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent() != null &&
                            current4.CurrentEffect >= 1)
                        {
                            Response.Init(LibraryParser.OutgoingRequest("ApplyEffectMessageComposer"));
                            Response.AppendInteger(current4.VirtualId);
                            Response.AppendInteger(current4.CurrentEffect);
                            Response.AppendInteger(0);
                            queuedServerMessage.AppendResponse(GetResponse());
                        }
                        ServerMessage serverMessage2 =
                            new ServerMessage(LibraryParser.OutgoingRequest("UpdateUserDataMessageComposer"));
                        serverMessage2.AppendInteger(current4.VirtualId);
                        serverMessage2.AppendString(current4.GetClient().GetHabbo().Look);
                        serverMessage2.AppendString(current4.GetClient().GetHabbo().Gender.ToLower());
                        serverMessage2.AppendString(current4.GetClient().GetHabbo().Motto);
                        serverMessage2.AppendInteger(current4.GetClient().GetHabbo().AchievementPoints);
                        if (CurrentLoadingRoom != null)
                            CurrentLoadingRoom.SendMessage(serverMessage2);
                    }
                }
                catch (Exception pException)
                {
                    ServerLogManager.LogException(pException, "Yupi.Messages.Handlers.Rooms.SendRoomData3");
                }
            }
            queuedServerMessage.SendResponse();
        }

        internal void EnterOnRoom()
        {
            if (Yupi.ShutdownStarted) return;

            uint id = Request.GetUInteger();
            string password = Request.GetString();
            PrepareRoomForUser(id, password);
        }

        internal void PrepareRoomForUser(uint id, string pWd, bool isReload = false)
        {
            try
            {
                if (Session == null || Session.GetHabbo() == null || Session.GetHabbo().LoadingRoom == id)
                    return;

                if (Yupi.ShutdownStarted)
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("server_shutdown"));
                    return;
                }

                Session.GetHabbo().LoadingRoom = id;
                QueuedServerMessage queuedServerMessage = new QueuedServerMessage(Session.GetConnection());

                Room room;
                if (Session.GetHabbo().InRoom)
                {
                    room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
                    if (room != null && room.GetRoomUserManager() != null)
                        room.GetRoomUserManager().RemoveUserFromRoom(Session, false, false);
                }
                room = Yupi.GetGame().GetRoomManager().LoadRoom(id);
                if (room == null)
                    return;

                if (room.UserCount >= room.RoomData.UsersMax && !Session.GetHabbo().HasFuse("fuse_enter_full_rooms") &&
                    Session.GetHabbo().Id != (ulong) room.RoomData.OwnerId)
                {
                    ServerMessage roomQueue = new ServerMessage(LibraryParser.OutgoingRequest("RoomsQueue"));

                    roomQueue.AppendInteger(2);
                    roomQueue.AppendString("visitors");
                    roomQueue.AppendInteger(2);
                    roomQueue.AppendInteger(1);
                    roomQueue.AppendString("visitors");
                    roomQueue.AppendInteger(room.UserCount - (int) room.RoomData.UsersNow);
                        // Currently people are in the queue -1 ()
                    roomQueue.AppendString("spectators");
                    roomQueue.AppendInteger(1);
                    roomQueue.AppendInteger(1);
                    roomQueue.AppendString("spectators");
                    roomQueue.AppendInteger(0);

                    Session.SendMessage(roomQueue);

                    //ClearRoomLoading();
                    return;

                    /* var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RoomEnterErrorMessageComposer"));
                     serverMessage.AppendInteger(1);
                     Session.SendMessage(serverMessage);
                     var message = new ServerMessage(LibraryParser.OutgoingRequest("OutOfRoomMessageComposer"));
                     Session.SendMessage(message);

                     ClearRoomLoading();
                     return;

                 */
                }

                CurrentLoadingRoom = room;

                if (!Session.GetHabbo().HasFuse("fuse_enter_any_room") && room.UserIsBanned(Session.GetHabbo().Id))
                {
                    if (!room.HasBanExpired(Session.GetHabbo().Id))
                    {
                        ClearRoomLoading();

                        ServerMessage serverMessage2 =
                            new ServerMessage(LibraryParser.OutgoingRequest("RoomEnterErrorMessageComposer"));
                        serverMessage2.AppendInteger(4);
                        Session.SendMessage(serverMessage2);
                        Response.Init(LibraryParser.OutgoingRequest("OutOfRoomMessageComposer"));
                        queuedServerMessage.AppendResponse(GetResponse());
                        queuedServerMessage.SendResponse();
                        return;
                    }
                    room.RemoveBan(Session.GetHabbo().Id);
                }
                Response.Init(LibraryParser.OutgoingRequest("PrepareRoomMessageComposer"));
                queuedServerMessage.AppendResponse(GetResponse());
                if (!isReload && !Session.GetHabbo().HasFuse("fuse_enter_any_room") &&
                    !room.CheckRightsDoorBell(Session, true, true,
                        room.RoomData.Group != null
                            ? room.RoomData.Group.Members.ContainsKey(Session.GetHabbo().Id)
                            : false) &&
                    !(Session.GetHabbo().IsTeleporting && Session.GetHabbo().TeleportingRoomId == id) &&
                    !Session.GetHabbo().IsHopping)
                {
                    if (room.RoomData.State == 1)
                    {
                        if (room.UserCount == 0)
                        {
                            Response.Init(LibraryParser.OutgoingRequest("DoorbellNoOneMessageComposer"));
                            queuedServerMessage.AppendResponse(GetResponse());
                        }
                        else
                        {
                            Response.Init(LibraryParser.OutgoingRequest("DoorbellMessageComposer"));
                            Response.AppendString("");
                            queuedServerMessage.AppendResponse(GetResponse());
                            ServerMessage serverMessage3 =
                                new ServerMessage(LibraryParser.OutgoingRequest("DoorbellMessageComposer"));
                            serverMessage3.AppendString(Session.GetHabbo().UserName);
                            room.SendMessageToUsersWithRights(serverMessage3);
                        }
                        queuedServerMessage.SendResponse();
                        return;
                    }
                    if (room.RoomData.State == 2 &&
                        !string.Equals(pWd, room.RoomData.PassWord, StringComparison.CurrentCultureIgnoreCase))
                    {
                        ClearRoomLoading();

                        Session.GetMessageHandler()
                            .GetResponse()
                            .Init(LibraryParser.OutgoingRequest("RoomErrorMessageComposer"));
                        Session.GetMessageHandler().GetResponse().AppendInteger(-100002);
                        Session.GetMessageHandler().SendResponse();

                        Session.GetMessageHandler()
                            .GetResponse()
                            .Init(LibraryParser.OutgoingRequest("OutOfRoomMessageComposer"));
                        Session.GetMessageHandler().GetResponse();
                        Session.GetMessageHandler().SendResponse();
                        return;
                    }
                }

                Session.GetHabbo().LoadingChecksPassed = true;
                queuedServerMessage.AddBytes(LoadRoomForUser().GetPacket);
                queuedServerMessage.SendResponse();

                if (Session.GetHabbo().RecentlyVisitedRooms.Contains(room.RoomId))
                    Session.GetHabbo().RecentlyVisitedRooms.Remove(room.RoomId);
                Session.GetHabbo().RecentlyVisitedRooms.AddFirst(room.RoomId);
            }
            catch (Exception e)
            {
                ServerLogManager.LogException("PrepareRoomForUser. RoomId: " + id + "; UserId: " +
                                              (Session?.GetHabbo().Id.ToString(CultureInfo.InvariantCulture) ?? "null") +
                                              Environment.NewLine + e);
            }
        }

        internal void ReqLoadRoomForUser()
        {
            LoadRoomForUser().SendResponse();
        }

        internal QueuedServerMessage LoadRoomForUser()
        {
            Room currentLoadingRoom = CurrentLoadingRoom;
            QueuedServerMessage queuedServerMessage = new QueuedServerMessage(Session.GetConnection());
            if (currentLoadingRoom == null || !Session.GetHabbo().LoadingChecksPassed) return queuedServerMessage;
            if (Session.GetHabbo().FavouriteGroup > 0u)
            {
                if (CurrentLoadingRoom.RoomData.Group != null &&
                    !CurrentLoadingRoom.LoadedGroups.ContainsKey(CurrentLoadingRoom.RoomData.Group.Id))
                    CurrentLoadingRoom.LoadedGroups.Add(CurrentLoadingRoom.RoomData.Group.Id,
                        CurrentLoadingRoom.RoomData.Group.Badge);
                if (!CurrentLoadingRoom.LoadedGroups.ContainsKey(Session.GetHabbo().FavouriteGroup) &&
                    Yupi.GetGame().GetGroupManager().GetGroup(Session.GetHabbo().FavouriteGroup) != null)
                    CurrentLoadingRoom.LoadedGroups.Add(Session.GetHabbo().FavouriteGroup,
                        Yupi.GetGame().GetGroupManager().GetGroup(Session.GetHabbo().FavouriteGroup).Badge);
            }
            Response.Init(LibraryParser.OutgoingRequest("RoomGroupMessageComposer"));
            Response.AppendInteger(CurrentLoadingRoom.LoadedGroups.Count);
            foreach (KeyValuePair<uint, string> guild1 in CurrentLoadingRoom.LoadedGroups)
            {
                Response.AppendInteger(guild1.Key);
                Response.AppendString(guild1.Value);
            }
            queuedServerMessage.AppendResponse(GetResponse());

            Response.Init(LibraryParser.OutgoingRequest("InitialRoomInfoMessageComposer"));
            Response.AppendString(currentLoadingRoom.RoomData.ModelName);
            Response.AppendInteger(currentLoadingRoom.RoomId);
            queuedServerMessage.AppendResponse(GetResponse());
            if (Session.GetHabbo().SpectatorMode)
            {
                Response.Init(LibraryParser.OutgoingRequest("SpectatorModeMessageComposer"));
                queuedServerMessage.AppendResponse(GetResponse());
            }

            if (currentLoadingRoom.RoomData.WallPaper != "0.0")
            {
                Response.Init(LibraryParser.OutgoingRequest("RoomSpacesMessageComposer"));
                Response.AppendString("wallpaper");
                Response.AppendString(currentLoadingRoom.RoomData.WallPaper);
                queuedServerMessage.AppendResponse(GetResponse());
            }
            if (currentLoadingRoom.RoomData.Floor != "0.0")
            {
                Response.Init(LibraryParser.OutgoingRequest("RoomSpacesMessageComposer"));
                Response.AppendString("floor");
                Response.AppendString(currentLoadingRoom.RoomData.Floor);
                queuedServerMessage.AppendResponse(GetResponse());
            }

            Response.Init(LibraryParser.OutgoingRequest("RoomSpacesMessageComposer"));
            Response.AppendString("landscape");
            Response.AppendString(currentLoadingRoom.RoomData.LandScape);
            queuedServerMessage.AppendResponse(GetResponse());

            if (currentLoadingRoom.CheckRights(Session, true))
            {
                Response.Init(LibraryParser.OutgoingRequest("RoomRightsLevelMessageComposer"));
                Response.AppendInteger(4);
                queuedServerMessage.AppendResponse(GetResponse());
                Response.Init(LibraryParser.OutgoingRequest("HasOwnerRightsMessageComposer"));
                queuedServerMessage.AppendResponse(GetResponse());
            }
            else if (currentLoadingRoom.CheckRights(Session, false, true))
            {
                Response.Init(LibraryParser.OutgoingRequest("RoomRightsLevelMessageComposer"));
                Response.AppendInteger(1);
                queuedServerMessage.AppendResponse(GetResponse());
            }
            else
            {
                Response.Init(LibraryParser.OutgoingRequest("RoomRightsLevelMessageComposer"));
                Response.AppendInteger(0);
                queuedServerMessage.AppendResponse(GetResponse());
            }

            Response.Init(LibraryParser.OutgoingRequest("RoomRatingMessageComposer"));
            Response.AppendInteger(currentLoadingRoom.RoomData.Score);
            Response.AppendBool(!Session.GetHabbo().RatedRooms.Contains(currentLoadingRoom.RoomId) &&
                                !currentLoadingRoom.CheckRights(Session, true));
            queuedServerMessage.AppendResponse(GetResponse());

            Response.Init(LibraryParser.OutgoingRequest("RoomUpdateMessageComposer"));
            Response.AppendInteger(currentLoadingRoom.RoomId);
            queuedServerMessage.AppendResponse(GetResponse());

            return queuedServerMessage;
        }

        internal void ClearRoomLoading()
        {
            if (Session == null || Session.GetHabbo() == null)
                return;
            Session.GetHabbo().LoadingRoom = 0u;
            Session.GetHabbo().LoadingChecksPassed = false;
        }

        internal void Move()
        {
            Room currentRoom = Session.GetHabbo().CurrentRoom;
            if (currentRoom == null)
                return;

            RoomUser roomUserByHabbo = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null || !roomUserByHabbo.CanWalk)
                return;

            int targetX = Request.GetInteger();
            int targetY = Request.GetInteger();

            if (targetX == roomUserByHabbo.X && targetY == roomUserByHabbo.Y)
                return;

            roomUserByHabbo.MoveTo(targetX, targetY);

            if (!roomUserByHabbo.RidingHorse)
                return;

            RoomUser roomUserByVirtualId =
                currentRoom.GetRoomUserManager().GetRoomUserByVirtualId((int) roomUserByHabbo.HorseId);

            roomUserByVirtualId.MoveTo(targetX, targetY);
        }

        internal void CanCreateRoom()
        {
            Response.Init(LibraryParser.OutgoingRequest("CanCreateRoomMessageComposer"));
            Response.AppendInteger(Session.GetHabbo().UsersRooms.Count >= 75 ? 1 : 0);
            Response.AppendInteger(75);
            SendResponse();
        }

        internal void CreateRoom()
        {
            if (Session.GetHabbo().UsersRooms.Count >= 75)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("user_has_more_then_75_rooms"));
                return;
            }
            if (Yupi.GetUnixTimeStamp() - Session.GetHabbo().LastSqlQuery < 20)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("user_create_room_flood_error"));
                return;
            }

            string name = Request.GetString();
            string description = Request.GetString();
            string roomModel = Request.GetString();
            int category = Request.GetInteger();
            int maxVisitors = Request.GetInteger();
            int tradeState = Request.GetInteger();

            RoomData data = Yupi.GetGame()
                .GetRoomManager()
                .CreateRoom(Session, name, description, roomModel, category, maxVisitors, tradeState);
            if (data == null)
                return;

            Session.GetHabbo().LastSqlQuery = Yupi.GetUnixTimeStamp();
            Response.Init(LibraryParser.OutgoingRequest("OnCreateRoomInfoMessageComposer"));
            Response.AppendInteger(data.Id);
            Response.AppendString(data.Name);
            SendResponse();
        }

        internal void GetRoomEditData()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Convert.ToUInt32(Request.GetInteger()));
            if (room == null)
                return;

            GetResponse().Init(LibraryParser.OutgoingRequest("RoomSettingsDataMessageComposer"));
            GetResponse().AppendInteger(room.RoomId);
            GetResponse().AppendString(room.RoomData.Name);
            GetResponse().AppendString(room.RoomData.Description);
            GetResponse().AppendInteger(room.RoomData.State);
            GetResponse().AppendInteger(room.RoomData.Category);
            GetResponse().AppendInteger(room.RoomData.UsersMax);
            GetResponse()
                .AppendInteger(room.RoomData.Model.MapSizeX*room.RoomData.Model.MapSizeY > 200 ? 50 : 25);

            GetResponse().AppendInteger(room.TagCount);
            foreach (string s in room.RoomData.Tags)
            {
                GetResponse().AppendString(s);
            }
            GetResponse().AppendInteger(room.RoomData.TradeState);
            GetResponse().AppendInteger(room.RoomData.AllowPets);
            GetResponse().AppendInteger(room.RoomData.AllowPetsEating);
            GetResponse().AppendInteger(room.RoomData.AllowWalkThrough);
            GetResponse().AppendInteger(room.RoomData.HideWall);
            GetResponse().AppendInteger(room.RoomData.WallThickness);
            GetResponse().AppendInteger(room.RoomData.FloorThickness);
            GetResponse().AppendInteger(room.RoomData.ChatType);
            GetResponse().AppendInteger(room.RoomData.ChatBalloon);
            GetResponse().AppendInteger(room.RoomData.ChatSpeed);
            GetResponse().AppendInteger(room.RoomData.ChatMaxDistance);
            GetResponse().AppendInteger(room.RoomData.ChatFloodProtection > 2 ? 2 : room.RoomData.ChatFloodProtection);
            GetResponse().AppendBool(false); //allow_dyncats_checkbox
            GetResponse().AppendInteger(room.RoomData.WhoCanMute);
            GetResponse().AppendInteger(room.RoomData.WhoCanKick);
            GetResponse().AppendInteger(room.RoomData.WhoCanBan);
            SendResponse();
        }

        internal void RoomSettingsOkComposer(uint roomId)
        {
            GetResponse().Init(LibraryParser.OutgoingRequest("RoomSettingsSavedMessageComposer"));
            GetResponse().AppendInteger(roomId);
            SendResponse();
        }

        internal void RoomUpdatedOkComposer(uint roomId)
        {
            GetResponse().Init(LibraryParser.OutgoingRequest("RoomUpdateMessageComposer"));
            GetResponse().AppendInteger(roomId);
            SendResponse();
        }

        internal static ServerMessage RoomFloorAndWallComposer(Room room)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RoomFloorWallLevelsMessageComposer"));
            serverMessage.AppendBool(room.RoomData.HideWall);
            serverMessage.AppendInteger(room.RoomData.WallThickness);
            serverMessage.AppendInteger(room.RoomData.FloorThickness);
            return serverMessage;
        }

        internal static ServerMessage SerializeRoomChatOption(Room room)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RoomChatOptionsMessageComposer"));
            serverMessage.AppendInteger(room.RoomData.ChatType);
            serverMessage.AppendInteger(room.RoomData.ChatBalloon);
            serverMessage.AppendInteger(room.RoomData.ChatSpeed);
            serverMessage.AppendInteger(room.RoomData.ChatMaxDistance);
            serverMessage.AppendInteger(room.RoomData.ChatFloodProtection);
            return serverMessage;
        }

        internal void ParseRoomDataInformation()
        {
            uint id = Request.GetUInteger();
            int num = Request.GetInteger();
            int num2 = Request.GetInteger();
            Room room = Yupi.GetGame().GetRoomManager().LoadRoom(id);
            if (num == 0 && num2 == 1)
            {
                SerializeRoomInformation(room, false);
                return;
            }
            if (num == 1 && num2 == 0)
            {
                SerializeRoomInformation(room, true);
                return;
            }
            SerializeRoomInformation(room, true);
        }

        internal void SerializeRoomInformation(Room room, bool show)
        {
            if (room == null)
                return;
            room.RoomData.SerializeRoomData(GetResponse(), Session, true, null, show);
            SendResponse();

            DataTable table;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery($"SELECT user_id FROM rooms_rights WHERE room_id={room.RoomId}");
                table = commitableQueryReactor.GetTable();
            }
            Response.Init(LibraryParser.OutgoingRequest("LoadRoomRightsListMessageComposer"));
            GetResponse().AppendInteger(room.RoomData.Id);
            GetResponse().AppendInteger(table.Rows.Count);

            foreach (
                Habbo habboForId in
                    table.Rows.Cast<DataRow>()
                        .Select(dataRow => Yupi.GetHabboById((uint) dataRow[0]))
                        .Where(habboForId => habboForId != null))
            {
                GetResponse().AppendInteger(habboForId.Id);
                GetResponse().AppendString(habboForId.UserName);
            }
            SendResponse();
        }

        internal void SaveRoomData()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            Request.GetInteger();

            string oldName = room.RoomData.Name;
            room.RoomData.Name = Request.GetString();
            if (room.RoomData.Name.Length < 3)
            {
                room.RoomData.Name = oldName;
                return;
            }

            room.RoomData.Description = Request.GetString();
            room.RoomData.State = Request.GetInteger();
            if (room.RoomData.State < 0 || room.RoomData.State > 2)
            {
                room.RoomData.State = 0;
                return;
            }
            room.RoomData.PassWord = Request.GetString();
            room.RoomData.UsersMax = Request.GetUInteger();
            room.RoomData.Category = Request.GetInteger();
            uint tagCount = Request.GetUInteger();

            if (tagCount > 2) return;
            List<string> tags = new List<string>();

            for (int i = 0; i < tagCount; i++)
                tags.Add(Request.GetString().ToLower());

            room.RoomData.TradeState = Request.GetInteger();
            room.RoomData.AllowPets = Request.GetBool();
            room.RoomData.AllowPetsEating = Request.GetBool();
            room.RoomData.AllowWalkThrough = Request.GetBool();
            room.RoomData.HideWall = Request.GetBool();
            room.RoomData.WallThickness = Request.GetInteger();
            if (room.RoomData.WallThickness < -2 || room.RoomData.WallThickness > 1) room.RoomData.WallThickness = 0;

            room.RoomData.FloorThickness = Request.GetInteger();
            if (room.RoomData.FloorThickness < -2 || room.RoomData.FloorThickness > 1) room.RoomData.FloorThickness = 0;

            room.RoomData.WhoCanMute = Request.GetInteger();
            room.RoomData.WhoCanKick = Request.GetInteger();
            room.RoomData.WhoCanBan = Request.GetInteger();
            room.RoomData.ChatType = Request.GetInteger();
            room.RoomData.ChatBalloon = Request.GetUInteger();
            room.RoomData.ChatSpeed = Request.GetUInteger();
            room.RoomData.ChatMaxDistance = Request.GetUInteger();
            if (room.RoomData.ChatMaxDistance > 90) room.RoomData.ChatMaxDistance = 90;

            room.RoomData.ChatFloodProtection = Request.GetUInteger(); //chat_flood_sensitivity
            if (room.RoomData.ChatFloodProtection > 2) room.RoomData.ChatFloodProtection = 2;

            Request.GetBool(); //allow_dyncats_checkbox
            PublicCategory flatCat = Yupi.GetGame().GetNavigator().GetFlatCat(room.RoomData.Category);
            if (flatCat == null || flatCat.MinRank > Session.GetHabbo().Rank) room.RoomData.Category = 0;

            room.ClearTags();
            room.AddTagRange(tags);

            RoomSettingsOkComposer(room.RoomId);
            RoomUpdatedOkComposer(room.RoomId);
            Session.GetHabbo().CurrentRoom.SendMessage(RoomFloorAndWallComposer(room));
            Session.GetHabbo().CurrentRoom.SendMessage(SerializeRoomChatOption(room));
            room.RoomData.SerializeRoomData(Response, Session, false, true);
        }

        internal void GetBannedUsers()
        {
            uint num = Request.GetUInteger();
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(num);
            if (room == null)
                return;
            List<uint> list = room.BannedUsers();
            Response.Init(LibraryParser.OutgoingRequest("RoomBannedListMessageComposer"));
            Response.AppendInteger(num);
            Response.AppendInteger(list.Count);
            foreach (uint current in list)
            {
                Response.AppendInteger(current);
                Response.AppendString(Yupi.GetHabboById(current) != null
                    ? Yupi.GetHabboById(current).UserName
                    : "Undefined");
            }
            SendResponse();
        }

        internal void UsersWithRights()
        {
            Response.Init(LibraryParser.OutgoingRequest("LoadRoomRightsListMessageComposer"));
            Response.AppendInteger(Session.GetHabbo().CurrentRoom.RoomId);
            Response.AppendInteger(Session.GetHabbo().CurrentRoom.UsersWithRights.Count);
            foreach (uint current in Session.GetHabbo().CurrentRoom.UsersWithRights)
            {
                Habbo habboForId = Yupi.GetHabboById(current);
                Response.AppendInteger(current);
                Response.AppendString(habboForId == null ? "Undefined" : habboForId.UserName);
            }
            SendResponse();
        }

        internal void UnbanUser()
        {
            uint num = Request.GetUInteger();
            uint num2 = Request.GetUInteger();
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(num2);
            if (room == null)
                return;
            room.Unban(num);
            Response.Init(LibraryParser.OutgoingRequest("RoomUnbanUserMessageComposer"));
            Response.AppendInteger(num2);
            Response.AppendInteger(num);
            SendResponse();
        }

        internal void GiveRights()
        {
            uint num = Request.GetUInteger();
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(num);
            if (!room.CheckRights(Session, true))
                return;
            if (room.UsersWithRights.Contains(num))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("no_room_rights_error"));
                return;
            }
            if (num == 0)
            {
                return;
            }
            room.UsersWithRights.Add(num);
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(string.Concat(
                    "INSERT INTO rooms_rights (room_id,user_id) VALUES (", room.RoomId, ",", num, ")"));
            if (roomUserByHabbo != null && !roomUserByHabbo.IsBot)
            {
                Response.Init(LibraryParser.OutgoingRequest("GiveRoomRightsMessageComposer"));
                Response.AppendInteger(room.RoomId);
                Response.AppendInteger(roomUserByHabbo.GetClient().GetHabbo().Id);
                Response.AppendString(roomUserByHabbo.GetClient().GetHabbo().UserName);
                SendResponse();
                roomUserByHabbo.UpdateNeeded = true;
                if (!roomUserByHabbo.IsBot)
                {
                    roomUserByHabbo.AddStatus("flatctrl 1", "");
                    Response.Init(LibraryParser.OutgoingRequest("RoomRightsLevelMessageComposer"));
                    Response.AppendInteger(1);
                    roomUserByHabbo.GetClient().SendMessage(GetResponse());
                }
            }
            UsersWithRights();
        }

        internal void TakeRights()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            StringBuilder stringBuilder = new StringBuilder();
            int num = Request.GetInteger();

            {
                for (int i = 0; i < num; i++)
                {
                    if (i > 0)
                        stringBuilder.Append(" OR ");
                    uint num2 = Request.GetUInteger();
                    if (room.UsersWithRights.Contains(num2))
                        room.UsersWithRights.Remove(num2);
                    stringBuilder.Append(string.Concat("room_id = '", room.RoomId, "' AND user_id = '", num2, "'"));
                    RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(num2);
                    if (roomUserByHabbo != null && !roomUserByHabbo.IsBot)
                    {
                        Response.Init(LibraryParser.OutgoingRequest("RoomRightsLevelMessageComposer"));
                        Response.AppendInteger(0);
                        roomUserByHabbo.GetClient().SendMessage(GetResponse());
                        roomUserByHabbo.RemoveStatus("flatctrl 1");
                        roomUserByHabbo.UpdateNeeded = true;
                    }
                    Response.Init(LibraryParser.OutgoingRequest("RemoveRightsMessageComposer"));
                    Response.AppendInteger(room.RoomId);
                    Response.AppendInteger(num2);
                    SendResponse();
                }
                UsersWithRights();
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    commitableQueryReactor.RunFastQuery($"DELETE FROM rooms_rights WHERE {stringBuilder}");
            }
        }

        internal void TakeAllRights()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            DataTable table;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery($"SELECT user_id FROM rooms_rights WHERE room_id={room.RoomId}");
                table = commitableQueryReactor.GetTable();
            }
            foreach (DataRow dataRow in table.Rows)
            {
                uint num = (uint) dataRow[0];
                RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(num);
                Response.Init(LibraryParser.OutgoingRequest("RemoveRightsMessageComposer"));
                Response.AppendInteger(room.RoomId);
                Response.AppendInteger(num);
                SendResponse();
                if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
                    continue;
                Response.Init(LibraryParser.OutgoingRequest("RoomRightsLevelMessageComposer"));
                Response.AppendInteger(0);
                roomUserByHabbo.GetClient().SendMessage(GetResponse());
                roomUserByHabbo.RemoveStatus("flatctrl 1");
                roomUserByHabbo.UpdateNeeded = true;
            }
            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                queryreactor2.RunFastQuery($"DELETE FROM rooms_rights WHERE room_id = {room.RoomId}");
            room.UsersWithRights.Clear();
            UsersWithRights();
        }

        internal void KickUser()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;

            if (!room.CheckRights(Session) && room.RoomData.WhoCanKick != 2 &&
                Session.GetHabbo().Rank < Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]))
                return;

            uint pId = Request.GetUInteger();
            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(pId);
            if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
                return;
            if (room.CheckRights(roomUserByHabbo.GetClient(), true) ||
                roomUserByHabbo.GetClient().GetHabbo().HasFuse("fuse_mod") ||
                roomUserByHabbo.GetClient().GetHabbo().HasFuse("fuse_no_kick"))
                return;
            room.GetRoomUserManager().RemoveUserFromRoom(roomUserByHabbo.GetClient(), true, true);
            roomUserByHabbo.GetClient().CurrentRoomUserId = -1;
        }

        internal void BanUser()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || (room.RoomData.WhoCanBan == 0 && !room.CheckRights(Session, true)) ||
                (room.RoomData.WhoCanBan == 1 && !room.CheckRights(Session)))
                return;
            int num = Request.GetInteger();
            Request.GetUInteger();
            string text = Request.GetString();
            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Convert.ToUInt32(num));
            if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
                return;
            if (roomUserByHabbo.GetClient().GetHabbo().HasFuse("fuse_mod") ||
                roomUserByHabbo.GetClient().GetHabbo().HasFuse("fuse_no_kick"))
                return;
            long time = 0L;
            if (text.ToLower().Contains("hour"))
                time = 3600L;
            else if (text.ToLower().Contains("day"))
                time = 86400L;
            else if (text.ToLower().Contains("perm"))
                time = 788922000L;
            room.AddBan(num, time);
            room.GetRoomUserManager().RemoveUserFromRoom(roomUserByHabbo.GetClient(), true, true);
            Session.CurrentRoomUserId = -1;
        }

        internal void SetHomeRoom()
        {
            uint roomId = Request.GetUInteger();
            RoomData data = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);

            if (roomId != 0 && data == null)
            {
                Session.GetHabbo().HomeRoom = roomId
                    ;
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    commitableQueryReactor.RunFastQuery(string.Concat("UPDATE users SET home_room = ", roomId,
                        " WHERE id = ", Session.GetHabbo().Id));

                Response.Init(LibraryParser.OutgoingRequest("HomeRoomMessageComposer"));
                Response.AppendInteger(roomId);
                Response.AppendInteger(0);
                SendResponse();
            }
        }

        internal void DeleteRoom()
        {
            uint roomId = Request.GetUInteger();
            if (Session == null || Session.GetHabbo() == null || Session.GetHabbo().UsersRooms == null)
                return;
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);
            if (room == null)
                return;
            if (room.RoomData.Owner != Session.GetHabbo().UserName && Session.GetHabbo().Rank <= 6u)
                return;
            if (Session.GetHabbo().GetInventoryComponent() != null)
                Session.GetHabbo()
                    .GetInventoryComponent()
                    .AddItemArray(room.GetRoomItemHandler().RemoveAllFurniture(Session));
            RoomData roomData = room.RoomData;
            Yupi.GetGame().GetRoomManager().UnloadRoom(room, "Delete room");
            Yupi.GetGame().GetRoomManager().QueueVoteRemove(roomData);
            if (roomData == null || Session == null)
                return;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.RunFastQuery($"DELETE FROM rooms_data WHERE id = {roomId}");
                commitableQueryReactor.RunFastQuery($"DELETE FROM users_favorites WHERE room_id = {roomId}");
                commitableQueryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE room_id = {roomId}");
                commitableQueryReactor.RunFastQuery($"DELETE FROM rooms_rights WHERE room_id = {roomId}");
                commitableQueryReactor.RunFastQuery($"UPDATE users SET home_room = '0' WHERE home_room = {roomId}");
            }
            if (Session.GetHabbo().Rank > 5u && Session.GetHabbo().UserName != roomData.Owner)
                Yupi.GetGame()
                    .GetModerationTool()
                    .LogStaffEntry(Session.GetHabbo().UserName, roomData.Name, "Room deletion",
                        $"Deleted room ID {roomData.Id}");
            RoomData roomData2 = (
                from p in Session.GetHabbo().UsersRooms
                where p.Id == roomId
                select p).SingleOrDefault();
            if (roomData2 != null)
                Session.GetHabbo().UsersRooms.Remove(roomData2);
        }

        internal void LookAt()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            roomUserByHabbo.UnIdle();
            int x = Request.GetInteger();
            int y = Request.GetInteger();
            if (x == roomUserByHabbo.X && y == roomUserByHabbo.Y)
                return;
            int rotation = PathFinder.CalculateRotation(roomUserByHabbo.X, roomUserByHabbo.Y, x, y);
            roomUserByHabbo.SetRot(rotation, false);
            roomUserByHabbo.UpdateNeeded = true;

            if (!roomUserByHabbo.RidingHorse)
                return;
            RoomUser roomUserByVirtualId =
                Session.GetHabbo()
                    .CurrentRoom.GetRoomUserManager()
                    .GetRoomUserByVirtualId(Convert.ToInt32(roomUserByHabbo.HorseId));
            roomUserByVirtualId.SetRot(rotation, false);
            roomUserByVirtualId.UpdateNeeded = true;
        }

        internal void StartTyping()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("TypingStatusMessageComposer"));
            serverMessage.AppendInteger(roomUserByHabbo.VirtualId);
            serverMessage.AppendInteger(1);
            room.SendMessage(serverMessage);
        }

        internal void StopTyping()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("TypingStatusMessageComposer"));
            serverMessage.AppendInteger(roomUserByHabbo.VirtualId);
            serverMessage.AppendInteger(0);
            room.SendMessage(serverMessage);
        }

        internal void IgnoreUser()
        {
            if (Session.GetHabbo().CurrentRoom == null)
                return;
            string text = Request.GetString();
            Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(text).GetHabbo();
            if (habbo == null)
                return;
            if (Session.GetHabbo().MutedUsers.Contains(habbo.Id) || habbo.Rank > 4u)
                return;
            Session.GetHabbo().MutedUsers.Add(habbo.Id);
            Response.Init(LibraryParser.OutgoingRequest("UpdateIgnoreStatusMessageComposer"));
            Response.AppendInteger(1);
            Response.AppendString(text);
            SendResponse();
        }

        internal void UnignoreUser()
        {
            if (Session.GetHabbo().CurrentRoom == null)
                return;
            string text = Request.GetString();
            Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(text).GetHabbo();
            if (habbo == null)
                return;
            if (!Session.GetHabbo().MutedUsers.Contains(habbo.Id))
                return;
            Session.GetHabbo().MutedUsers.Remove(habbo.Id);
            Response.Init(LibraryParser.OutgoingRequest("UpdateIgnoreStatusMessageComposer"));
            Response.AppendInteger(3);
            Response.AppendString(text);
            SendResponse();
        }

        internal void CanCreateRoomEvent()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            bool b = true;
            int i = 0;
            if (room.RoomData.State != 0)
            {
                b = false;
                i = 3;
            }
            Response.AppendBool(b);
            Response.AppendInteger(i);
        }

        internal void Sign()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            roomUserByHabbo.UnIdle();
            int value = Request.GetInteger();
            roomUserByHabbo.AddStatus("sign", Convert.ToString(value));
            roomUserByHabbo.UpdateNeeded = true;
            roomUserByHabbo.SignTime = Yupi.GetUnixTimeStamp() + 5;
        }

        internal void InitRoomGroupBadges()
        {
            Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().LoadingRoom);
        }

        internal void RateRoom()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || Session.GetHabbo().RatedRooms.Contains(room.RoomId) ||
                room.CheckRights(Session, true))
                return;

            {
                switch (Request.GetInteger())
                {
                    case -1:
                        room.RoomData.Score--;
                        room.RoomData.Score--;
                        break;

                    case 0:
                        return;

                    case 1:
                        room.RoomData.Score++;
                        room.RoomData.Score++;
                        break;

                    default:
                        return;
                }
                Yupi.GetGame().GetRoomManager().QueueVoteAdd(room.RoomData);
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    commitableQueryReactor.RunFastQuery(string.Concat("UPDATE rooms_data SET score = ",
                        room.RoomData.Score, " WHERE id = ", room.RoomId));
                Session.GetHabbo().RatedRooms.Add(room.RoomId);
                Response.Init(LibraryParser.OutgoingRequest("RoomRatingMessageComposer"));
                Response.AppendInteger(room.RoomData.Score);
                Response.AppendBool(room.CheckRights(Session, true));
                SendResponse();
            }
        }

        internal void Dance()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            roomUserByHabbo.UnIdle();

            uint num = Request.GetUInteger();

            if (num > 4)
                num = 0;

            if (num > 0 && roomUserByHabbo.CarryItemId > 0)
                roomUserByHabbo.CarryItem(0);

            roomUserByHabbo.DanceId = num;

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("DanceStatusMessageComposer"));

            serverMessage.AppendInteger(roomUserByHabbo.VirtualId);
            serverMessage.AppendInteger(num);
            room.SendMessage(serverMessage);
        }

        internal void AnswerDoorbell()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session))
                return;
            string userName = Request.GetString();
            bool flag = Request.GetBool();
            GameClient clientByUserName = Yupi.GetGame().GetClientManager().GetClientByUserName(userName);
            if (clientByUserName == null)
                return;
            if (flag)
            {
                clientByUserName.GetHabbo().LoadingChecksPassed = true;
                clientByUserName.GetMessageHandler()
                    .Response.Init(LibraryParser.OutgoingRequest("DoorbellOpenedMessageComposer"));
                clientByUserName.GetMessageHandler().Response.AppendString("");
                clientByUserName.GetMessageHandler().SendResponse();
                return;
            }
            if (clientByUserName.GetHabbo().CurrentRoomId != Session.GetHabbo().CurrentRoomId)
            {
                clientByUserName.GetMessageHandler()
                    .Response.Init(LibraryParser.OutgoingRequest("DoorbellNoOneMessageComposer"));
                clientByUserName.GetMessageHandler().Response.AppendString("");
                clientByUserName.GetMessageHandler().SendResponse();
            }
        }

        internal void AlterRoomFilter()
        {
            uint num = Request.GetUInteger();
            bool flag = Request.GetBool();
            string text = Request.GetString();
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            if (!flag)
            {
                if (!room.WordFilter.Contains(text))
                    return;
                room.WordFilter.Remove(text);
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    commitableQueryReactor.SetQuery("DELETE FROM rooms_wordfilter WHERE room_id = @id AND word = @word");
                    commitableQueryReactor.AddParameter("id", num);
                    commitableQueryReactor.AddParameter("word", text);
                    commitableQueryReactor.RunQuery();
                    return;
                }
            }
            if (room.WordFilter.Contains(text))
                return;
            if (text.Contains("+"))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("character_error_plus"));
                return;
            }
            room.WordFilter.Add(text);
            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryreactor2.SetQuery("INSERT INTO rooms_wordfilter (room_id, word) VALUES (@id, @word);");
                queryreactor2.AddParameter("id", num);
                queryreactor2.AddParameter("word", text);
                queryreactor2.RunQuery();
            }
        }

        internal void GetRoomFilter()
        {
            uint roomId = Request.GetUInteger();
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            ServerMessage serverMessage = new ServerMessage();
            serverMessage.Init(LibraryParser.OutgoingRequest("RoomLoadFilterMessageComposer"));
            serverMessage.AppendInteger(room.WordFilter.Count);
            foreach (string current in room.WordFilter)
                serverMessage.AppendString(current);
            Response = serverMessage;
            SendResponse();
        }

        internal void ApplyRoomEffect()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            UserItem item = Session.GetHabbo().GetInventoryComponent().GetItem(Request.GetUInteger());
            if (item == null)
                return;
            string type = "floor";

            if (item.BaseItem.Name.ToLower().Contains("wallpaper"))
                type = "wallpaper";
            else if (item.BaseItem.Name.ToLower().Contains("landscape"))
                type = "landscape";

            switch (type)
            {
                case "floor":

                    room.RoomData.Floor = item.ExtraData;

                    Yupi.GetGame()
                        .GetAchievementManager()
                        .ProgressUserAchievement(Session, "ACH_RoomDecoFloor", 1);
                    break;

                case "wallpaper":

                    room.RoomData.WallPaper = item.ExtraData;

                    Yupi.GetGame()
                        .GetAchievementManager()
                        .ProgressUserAchievement(Session, "ACH_RoomDecoWallpaper", 1);
                    break;

                case "landscape":

                    room.RoomData.LandScape = item.ExtraData;

                    Yupi.GetGame()
                        .GetAchievementManager()
                        .ProgressUserAchievement(Session, "ACH_RoomDecoLandscape", 1);
                    break;
            }
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(string.Concat("UPDATE rooms_data SET ", type,
                    " = @extradata WHERE id = ", room.RoomId));
                commitableQueryReactor.AddParameter("extradata", item.ExtraData);
                commitableQueryReactor.RunQuery();
                commitableQueryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE id={item.Id} LIMIT 1");
            }
            Session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RoomSpacesMessageComposer"));
            serverMessage.AppendString(type);
            serverMessage.AppendString(item.ExtraData);
            room.SendMessage(serverMessage);
        }

        internal void PromoteRoom()
        {
            uint pageId = Request.GetUInteger();
            uint item = Request.GetUInteger();

            CatalogPage page2 = Yupi.GetGame().GetCatalog().GetPage(pageId);
            CatalogItem catalogItem = page2?.GetItem(item);

            if (catalogItem == null)
                return;

            uint num = Request.GetUInteger();
            string text = Request.GetString();

            Request.GetBool();

            string text2 = Request.GetString();

            int category = Request.GetInteger();

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(num) ?? new Room();

            room.Start(Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(num), true);

            if (!room.CheckRights(Session, true))
                return;

            if (catalogItem.CreditsCost > 0)
            {
                if (catalogItem.CreditsCost > Session.GetHabbo().Credits)
                    return;

                Session.GetHabbo().Credits -= catalogItem.CreditsCost;
                Session.GetHabbo().UpdateCreditsBalance();
            }

            if (catalogItem.DucketsCost > 0)
            {
                if (catalogItem.DucketsCost > Session.GetHabbo().Duckets)
                    return;

                Session.GetHabbo().Duckets -= catalogItem.DucketsCost;
                Session.GetHabbo().UpdateActivityPointsBalance();
            }

            if (catalogItem.DiamondsCost > 0)
            {
                if (catalogItem.DiamondsCost > Session.GetHabbo().Diamonds)
                    return;

                Session.GetHabbo().Diamonds -= catalogItem.DiamondsCost;
                Session.GetHabbo().UpdateSeasonalCurrencyBalance();
            }

            Session.SendMessage(CatalogPageComposer.PurchaseOk(catalogItem, catalogItem.Items));

            if (room.RoomData.Event != null && !room.RoomData.Event.HasExpired)
            {
                room.RoomData.Event.Time = Yupi.GetUnixTimeStamp();

                Yupi.GetGame().GetRoomEvents().SerializeEventInfo(room.RoomId);
            }
            else
            {
                Yupi.GetGame().GetRoomEvents().AddNewEvent(room.RoomId, text, text2, Session, 7200, category);
                Yupi.GetGame().GetRoomEvents().SerializeEventInfo(room.RoomId);
            }

            Session.GetHabbo().GetBadgeComponent().GiveBadge("RADZZ", true, Session);
        }

        internal void GetPromotionableRooms()
        {
            ServerMessage serverMessage = new ServerMessage();
            serverMessage.Init(LibraryParser.OutgoingRequest("CatalogPromotionGetRoomsMessageComposer"));
            serverMessage.AppendBool(true);
            serverMessage.AppendInteger(Session.GetHabbo().UsersRooms.Count);

            foreach (RoomData current in Session.GetHabbo().UsersRooms)
            {
                serverMessage.AppendInteger(current.Id);
                serverMessage.AppendString(current.Name);
                serverMessage.AppendBool(false);
            }

            Response = serverMessage;
            SendResponse();
        }

        internal void SaveHeightmap()
        {
            if (Session?.GetHabbo() != null)
            {
                Room room = Session.GetHabbo().CurrentRoom;

                if (room == null)
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("user_is_not_in_room"));
                    return;
                }

                if (!room.CheckRights(Session, true))
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("user_is_not_his_room"));
                    return;
                }

                string heightMap = Request.GetString();
                int doorX = Request.GetInteger();
                int doorY = Request.GetInteger();
                int doorOrientation = Request.GetInteger();
                int wallThickness = Request.GetInteger();
                int floorThickness = Request.GetInteger();
                int wallHeight = Request.GetInteger();

                if (heightMap.Length < 2)
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("invalid_room_length"));
                    return;
                }

                if (wallThickness < -2 || wallThickness > 1)
                    wallThickness = 0;

                if (floorThickness < -2 || floorThickness > 1)
                    floorThickness = 0;

                if (doorOrientation < 0 || doorOrientation > 8)
                    doorOrientation = 2;

                if (wallHeight < -1 || wallHeight > 16)
                    wallHeight = -1;

                char[] validLetters =
                {
                    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g',
                    'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', '\r'
                };

                if (heightMap.Any(letter => !validLetters.Contains(letter)))
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("user_floor_editor_error"));

                    return;
                }

                if (heightMap.Last() == Convert.ToChar(13))
                    heightMap = heightMap.Remove(heightMap.Length - 1);

                if (heightMap.Length > 1800)
                {
                    ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
                    message.AppendString("floorplan_editor.error");
                    message.AppendInteger(1);
                    message.AppendString("errors");
                    message.AppendString(
                        "(general): too large height (max 64 tiles)\r(general): too large area (max 1800 tiles)");
                    Session.SendMessage(message);

                    return;
                }

                if (heightMap.Split((char) 13).Length - 1 < doorY)
                {
                    ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
                    message.AppendString("floorplan_editor.error");
                    message.AppendInteger(1);
                    message.AppendString("errors");
                    message.AppendString("Y: Door is in invalid place.");
                    Session.SendMessage(message);

                    return;
                }

                string[] lines = heightMap.Split((char) 13);
                int lineWidth = lines[0].Length;
                for (int i = 1; i < lines.Length; i++)
                    if (lines[i].Length != lineWidth)
                    {
                        ServerMessage message =
                            new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
                        message.AppendString("floorplan_editor.error");
                        message.AppendInteger(1);
                        message.AppendString("errors");
                        message.AppendString("(general): Line " + (i + 1) + " is of different length than line 1");
                        Session.SendMessage(message);

                        return;
                    }
                double doorZ = 0.0;
                char charDoor = lines[doorY][doorX];
                if (charDoor >= (char) 97 && charDoor <= 119) // a-w
                {
                    doorZ = charDoor - 87;
                }
                else
                {
                    double.TryParse(charDoor.ToString(), out doorZ);
                }
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    commitableQueryReactor.SetQuery(
                        "REPLACE INTO rooms_models_customs (roomid,door_x,door_y,door_z,door_dir,heightmap,poolmap) VALUES ('" +
                        room.RoomId + "', '" + doorX + "','" +
                        doorY + "','" + doorZ.ToString(CultureInfo.InvariantCulture).Replace(',', '.') + "','" +
                        doorOrientation + "',@newmodel,'')");
                    commitableQueryReactor.AddParameter("newmodel", heightMap);
                    commitableQueryReactor.RunQuery();

                    room.RoomData.WallHeight = wallHeight;
                    room.RoomData.WallThickness = wallThickness;
                    room.RoomData.FloorThickness = floorThickness;
                    room.RoomData.Model.DoorZ = doorZ;

                    Yupi.GetGame()
                        .GetAchievementManager()
                        .ProgressUserAchievement(Session, "ACH_RoomDecoHoleFurniCount", 1);

                    commitableQueryReactor.RunFastQuery(
                        $"UPDATE rooms_data SET model_name = 'custom', wallthick = '{wallThickness}', floorthick = '{floorThickness}', walls_height = '{wallHeight}' WHERE id = {room.RoomId};");
                    RoomModel roomModel = new RoomModel(doorX, doorY, doorZ, doorOrientation, heightMap, "", false, "");
                    Yupi.GetGame().GetRoomManager().UpdateCustomModel(room.RoomId, roomModel);
                    room.ResetGameMap("custom", wallHeight, wallThickness, floorThickness);
                    Yupi.GetGame().GetRoomManager().UnloadRoom(room, "Reload floor");

                    ServerMessage forwardToRoom = new ServerMessage(LibraryParser.OutgoingRequest("RoomForwardMessageComposer"));
                    forwardToRoom.AppendInteger(room.RoomId);
                    Session.SendMessage(forwardToRoom);
                }
            }
        }

        internal void PlantMonsterplant(RoomItem mopla, Room room)
        {
            int rarity = 0, internalRarity = 0;

            if (room == null || mopla == null)
                return;

            if ((mopla.GetBaseItem().InteractionType != Interaction.Moplaseed) &&
                (mopla.GetBaseItem().InteractionType != Interaction.RareMoplaSeed))
                return;

            if (string.IsNullOrEmpty(mopla.ExtraData) || mopla.ExtraData == "0")
                rarity = 1;

            if (!string.IsNullOrEmpty(mopla.ExtraData) && mopla.ExtraData != "0")
                rarity = int.TryParse(mopla.ExtraData, out internalRarity) ? internalRarity : 1;

            int getX = mopla.X;
            int getY = mopla.Y;

            room.GetRoomItemHandler().RemoveFurniture(Session, mopla.Id, false);

            Pet pet = CatalogManager.CreatePet(Session.GetHabbo().Id, "Monsterplant", "pet_monster", "0", "0", rarity);

            Response.Init(LibraryParser.OutgoingRequest("SendMonsterplantIdMessageComposer"));
            Response.AppendInteger(pet.PetId);
            SendResponse();

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(string.Concat("UPDATE pets_data SET room_id = '", room.RoomId,
                    "', x = '", getX, "', y = '", getY, "' WHERE id = '", pet.PetId, "'"));

            pet.PlacedInRoom = true;
            pet.RoomId = room.RoomId;

            RoomBot bot = new RoomBot(pet.PetId, pet.OwnerId, pet.RoomId, AiType.Pet, "freeroam", pet.Name, "", pet.Look,
                getX, getY, 0.0, 4, null, null, "", 0, "");

            room.GetRoomUserManager().DeployBot(bot, pet);

            if (pet.DbState != DatabaseUpdateState.NeedsInsert)
                pet.DbState = DatabaseUpdateState.NeedsUpdate;

            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryreactor2.RunFastQuery($"DELETE FROM items_rooms WHERE id = {mopla.Id}");
                room.GetRoomUserManager().SavePets(queryreactor2);
            }
        }

        internal void KickBot()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            RoomUser roomUserByVirtualId = room.GetRoomUserManager().GetRoomUserByVirtualId(Request.GetInteger());

            if (roomUserByVirtualId == null || !roomUserByVirtualId.IsBot)
                return;

            room.GetRoomUserManager().RemoveBot(roomUserByVirtualId.VirtualId, true);
        }

        internal void PlacePet()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(Session, true)) ||
                !room.CheckRights(Session, true))
                return;

            uint petId = Request.GetUInteger();

            Pet pet = Session.GetHabbo().GetInventoryComponent().GetPet(petId);

            if (pet == null || pet.PlacedInRoom)
                return;

            int x = Request.GetInteger();
            int y = Request.GetInteger();

            if (!room.GetGameMap().CanWalk(x, y, false))
                return;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery("UPDATE pets_data SET room_id = '" + room.RoomId + "', x = '" + x +
                                                    "', y = '" + y + "' WHERE id = '" + petId + "'");

            pet.PlacedInRoom = true;
            pet.RoomId = room.RoomId;

            room.GetRoomUserManager()
                .DeployBot(
                    new RoomBot(pet.PetId, Convert.ToUInt32(pet.OwnerId), pet.RoomId, AiType.Pet, "freeroam", pet.Name,
                        "", pet.Look, x, y, 0.0, 4, null, null, "", 0, ""), pet);

            Session.GetHabbo().GetInventoryComponent().MovePetToRoom(pet.PetId);

            if (pet.DbState != DatabaseUpdateState.NeedsInsert)
                pet.DbState = DatabaseUpdateState.NeedsUpdate;

            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                room.GetRoomUserManager().SavePets(queryreactor2);

            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializePetInventory());
        }

        internal void UpdateEventInfo()
        {
            Request.GetInteger();
            string original = Request.GetString();
            string original2 = Request.GetString();
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true) || room.RoomData.Event == null)
                return;
            room.RoomData.Event.Name = original;
            room.RoomData.Event.Description = original2;
            Yupi.GetGame().GetRoomEvents().UpdateEvent(room.RoomData.Event);
        }

        internal void HandleBotSpeechList()
        {
            uint botId = Request.GetUInteger();
            int num2 = Request.GetInteger();
            int num3 = num2;

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            RoomUser bot = room.GetRoomUserManager().GetBot(botId);
            if (bot == null || !bot.IsBot)
                return;

            if (num3 == 2)
            {
                string text = bot.BotData.RandomSpeech == null ? string.Empty : string.Join("\n", bot.BotData.RandomSpeech);
                text += ";#;";
                text += bot.BotData.AutomaticChat ? "true" : "false";
                text += ";#;";
                text += bot.BotData.SpeechInterval;
                text += ";#;";
                text += bot.BotData.MixPhrases ? "true" : "false";

                ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("BotSpeechListMessageComposer"));
                serverMessage.AppendInteger(botId);
                serverMessage.AppendInteger(num2);
                serverMessage.AppendString(text);
                Response = serverMessage;
                SendResponse();
                return;
            }
            if (num3 != 5)
                return;

            ServerMessage serverMessage2 = new ServerMessage(LibraryParser.OutgoingRequest("BotSpeechListMessageComposer"));
            serverMessage2.AppendInteger(botId);
            serverMessage2.AppendInteger(num2);
            serverMessage2.AppendString(bot.BotData.Name);

            Response = serverMessage2;
            SendResponse();
        }

        internal void ManageBotActions()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            uint botId = Request.GetUInteger();
            int action = Request.GetInteger();

            string data = Yupi.FilterInjectionChars(Request.GetString());

            RoomUser bot = room.GetRoomUserManager().GetBot(botId);

            bool flag = false;

            switch (action)
            {
                case 1:
                    bot.BotData.Look = Session.GetHabbo().Look;
                    goto IL_439;
                case 2:
                    try
                    {
                        string[] array = data.Split(new[] {";#;"}, StringSplitOptions.None);

                        string[] speechsJunk =
                            array[0].Substring(0, array[0].Length > 1024 ? 1024 : array[0].Length)
                                .Split(Convert.ToChar(13));

                        bool speak = array[1] == "true";

                        uint speechDelay = uint.Parse(array[2]);

                        bool mix = array[3] == "true";
                        if (speechDelay < 7) speechDelay = 7;

                        string speechs =
                            speechsJunk.Where(
                                speech =>
                                    !string.IsNullOrEmpty(speech) &&
                                    (!speech.ToLower().Contains("update") || !speech.ToLower().Contains("set")))
                                .Aggregate(string.Empty,
                                    (current, speech) =>
                                        current +
                                        ServerUserChatTextHandler.FilterHtml(speech, Session.GetHabbo().GotCommand("ha")) +
                                        ";");

                        using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        {
                            commitableQueryReactor.SetQuery(
                                "UPDATE bots_data SET automatic_chat = @autochat, speaking_interval = @interval, mix_phrases = @mix_phrases, speech = @speech WHERE id = @botid");

                            commitableQueryReactor.AddParameter("autochat", speak ? "1" : "0");
                            commitableQueryReactor.AddParameter("interval", speechDelay);
                            commitableQueryReactor.AddParameter("mix_phrases", mix ? "1" : "0");
                            commitableQueryReactor.AddParameter("speech", speechs);
                            commitableQueryReactor.AddParameter("botid", botId);
                            commitableQueryReactor.RunQuery();
                        }
                        List<string> randomSpeech = speechs.Split(';').ToList();

                        room.GetRoomUserManager()
                            .UpdateBot(bot.VirtualId, bot, bot.BotData.Name, bot.BotData.Motto, bot.BotData.Look,
                                bot.BotData.Gender, randomSpeech, null, speak, speechDelay, mix);
                        flag = true;
                        goto IL_439;
                    }
                    catch (Exception e)
                    {
                        ServerLogManager.LogException(e.ToString());
                        return;
                    }
                case 3:
                    bot.BotData.WalkingMode = bot.BotData.WalkingMode == "freeroam" ? "stand" : "freeroam";
                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        commitableQueryReactor.SetQuery("UPDATE bots_data SET walk_mode = @walkmode WHERE id = @botid");
                        commitableQueryReactor.AddParameter("walkmode", bot.BotData.WalkingMode);
                        commitableQueryReactor.AddParameter("botid", botId);
                        commitableQueryReactor.RunQuery();
                    }
                    goto IL_439;
                case 4:
                    break;

                case 5:
                    string name = ServerUserChatTextHandler.FilterHtml(data, Session.GetHabbo().GotCommand("ha"));
                    if (name.Length < 15)
                        bot.BotData.Name = name;
                    else
                    {
                        BotErrorComposer(4);
                        break;
                    }

                    goto IL_439;
                default:
                    goto IL_439;
            }
            if (bot.BotData.DanceId > 0) bot.BotData.DanceId = 0;
            else
            {
                Random random = new Random();
                bot.DanceId = (uint) random.Next(1, 4);
                bot.BotData.DanceId = bot.DanceId;
            }
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("DanceStatusMessageComposer"));
            serverMessage.AppendInteger(bot.VirtualId);
            serverMessage.AppendInteger(bot.BotData.DanceId);
            Session.GetHabbo().CurrentRoom.SendMessage(serverMessage);
            IL_439:
            if (!flag)
            {
                ServerMessage serverMessage2 = new ServerMessage(LibraryParser.OutgoingRequest("SetRoomUserMessageComposer"));
                serverMessage2.AppendInteger(1);
                bot.Serialize(serverMessage2, room.GetGameMap().GotPublicPool);
                room.SendMessage(serverMessage2);
            }
        }

        internal void BotErrorComposer(int errorid)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("GeneralErrorHabboMessageComposer"));
            serverMessage.AppendInteger(errorid);
            Session.SendMessage(serverMessage);
        }

        internal void RoomOnLoad()
        {
            // TODO!
            Response.Init(LibraryParser.OutgoingRequest("SendRoomCampaignFurnitureMessageComposer"));
            Response.AppendInteger(0);
            SendResponse();
        }

        internal void MuteAll()
        {
            Room currentRoom = Session.GetHabbo().CurrentRoom;
            if (currentRoom == null || !currentRoom.CheckRights(Session, true))
                return;
            currentRoom.RoomMuted = !currentRoom.RoomMuted;

            Response.Init(LibraryParser.OutgoingRequest("RoomMuteStatusMessageComposer"));
            Response.AppendBool(currentRoom.RoomMuted);
            Session.SendMessage(Response);
        }

        internal void HomeRoom()
        {
            GetResponse().Init(LibraryParser.OutgoingRequest("HomeRoomMessageComposer"));
            GetResponse().AppendInteger(Session.GetHabbo().HomeRoom);
            GetResponse().AppendInteger(0);
            SendResponse();
        }

        internal void RemoveFavouriteRoom()
        {
            if (Session.GetHabbo() == null)
                return;
            uint num = Request.GetUInteger();
            Session.GetHabbo().FavoriteRooms.Remove(num);
            Response.Init(LibraryParser.OutgoingRequest("FavouriteRoomsUpdateMessageComposer"));
            Response.AppendInteger(num);
            Response.AppendBool(false);
            SendResponse();

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(string.Concat("DELETE FROM users_favorites WHERE user_id = ",
                    Session.GetHabbo().Id, " AND room_id = ", num));
        }

        internal void RoomUserAction()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            roomUserByHabbo.UnIdle();
            int num = Request.GetInteger();
            roomUserByHabbo.DanceId = 0;

            ServerMessage action = new ServerMessage(LibraryParser.OutgoingRequest("RoomUserActionMessageComposer"));
            action.AppendInteger(roomUserByHabbo.VirtualId);
            action.AppendInteger(num);
            room.SendMessage(action);

            if (num == 5)
            {
                roomUserByHabbo.IsAsleep = true;

                ServerMessage sleep = new ServerMessage(LibraryParser.OutgoingRequest("RoomUserIdleMessageComposer"));
                sleep.AppendInteger(roomUserByHabbo.VirtualId);
                sleep.AppendBool(roomUserByHabbo.IsAsleep);
                room.SendMessage(sleep);
            }
        }

        internal void GetRoomData1()
        {
        }

        internal void GetRoomData2()
        {
            try
            {
                if (Session?.GetConnection() != null)
                {
                    QueuedServerMessage queuedServerMessage = new QueuedServerMessage(Session.GetConnection());

                    if (Session.GetHabbo().LoadingRoom <= 0u || CurrentLoadingRoom == null)
                        return;

                    RoomData roomData = CurrentLoadingRoom.RoomData;

                    if (roomData == null)
                        return;

                    if (roomData.Model == null || CurrentLoadingRoom.GetGameMap() == null)
                    {
                        Session.SendMessage(new ServerMessage(LibraryParser.OutgoingRequest("OutOfRoomMessageComposer")));
                        ClearRoomLoading();
                    }
                    else
                    {
                        queuedServerMessage.AppendResponse(CurrentLoadingRoom.GetGameMap().GetNewHeightmap());
                        queuedServerMessage.AppendResponse(CurrentLoadingRoom.GetGameMap().Model.GetHeightmap());
                        queuedServerMessage.SendResponse();
                        GetRoomData3();
                    }
                }
            }
            catch (Exception ex)
            {
                ServerLogManager.LogException(ex, "Yupi.Messages.Handlers.Rooms.GetRoomData2");
            }
        }

        internal void GetRoomData3()
        {
            if (Session.GetHabbo().LoadingRoom <= 0u || !Session.GetHabbo().LoadingChecksPassed ||
                CurrentLoadingRoom == null || Session == null)
                return;

            if (CurrentLoadingRoom.RoomData.UsersNow + 1 > CurrentLoadingRoom.RoomData.UsersMax &&
                !Session.GetHabbo().HasFuse("fuse_enter_full_rooms"))
            {
                ServerMessage roomFull = new ServerMessage(LibraryParser.OutgoingRequest("RoomEnterErrorMessageComposer"));
                roomFull.AppendInteger(1);
                return;
            }

            QueuedServerMessage queuedServerMessage = new QueuedServerMessage(Session.GetConnection());
            RoomItem[] array = CurrentLoadingRoom.GetRoomItemHandler().FloorItems.Values.ToArray();
            RoomItem[] array2 = CurrentLoadingRoom.GetRoomItemHandler().WallItems.Values.ToArray();

            Response.Init(LibraryParser.OutgoingRequest("RoomFloorItemsMessageComposer"));

            if (CurrentLoadingRoom.RoomData.Group != null)
            {
                if (CurrentLoadingRoom.RoomData.Group.AdminOnlyDeco == 1u)
                {
                    Response.AppendInteger(CurrentLoadingRoom.RoomData.Group.Admins.Count + 1);

                    using (Dictionary<uint, GroupMember>.ValueCollection.Enumerator enumerator = CurrentLoadingRoom.RoomData.Group.Admins.Values.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            GroupMember current = enumerator.Current;

                            if (Yupi.GetHabboById(current.Id) == null)
                                continue;

                            Response.AppendInteger(current.Id);
                            Response.AppendString(Yupi.GetHabboById(current.Id).UserName);
                        }

                        goto IL_220;
                    }
                }

                Response.AppendInteger(CurrentLoadingRoom.RoomData.Group.Members.Count + 1);

                foreach (GroupMember current2 in CurrentLoadingRoom.RoomData.Group.Members.Values)
                {
                    Response.AppendInteger(current2.Id);
                    Response.AppendString(Yupi.GetHabboById(current2.Id).UserName);
                }

                IL_220:

                Response.AppendInteger(CurrentLoadingRoom.RoomData.OwnerId);
                Response.AppendString(CurrentLoadingRoom.RoomData.Owner);
            }
            else
            {
                Response.AppendInteger(1);
                Response.AppendInteger(CurrentLoadingRoom.RoomData.OwnerId);
                Response.AppendString(CurrentLoadingRoom.RoomData.Owner);
            }

            Response.AppendInteger(array.Length);

            foreach (RoomItem roomItem in array)
                roomItem.Serialize(Response);

            queuedServerMessage.AppendResponse(GetResponse());
            Response.Init(LibraryParser.OutgoingRequest("RoomWallItemsMessageComposer"));

            if (CurrentLoadingRoom.RoomData.Group != null)
            {
                if (CurrentLoadingRoom.RoomData.Group.AdminOnlyDeco == 1u)
                {
                    Response.AppendInteger(CurrentLoadingRoom.RoomData.Group.Admins.Count + 1);

                    using (Dictionary<uint, GroupMember>.ValueCollection.Enumerator enumerator3 = CurrentLoadingRoom.RoomData.Group.Admins.Values.GetEnumerator())
                    {
                        while (enumerator3.MoveNext())
                        {
                            GroupMember current3 = enumerator3.Current;

                            Response.AppendInteger(current3.Id);
                            Response.AppendString(Yupi.GetHabboById(current3.Id).UserName);
                        }

                        goto IL_423;
                    }
                }

                Response.AppendInteger(CurrentLoadingRoom.RoomData.Group.Members.Count + 1);

                foreach (GroupMember current4 in CurrentLoadingRoom.RoomData.Group.Members.Values)
                {
                    Response.AppendInteger(current4.Id);
                    Response.AppendString(Yupi.GetHabboById(current4.Id).UserName);
                }

                IL_423:
                Response.AppendInteger(CurrentLoadingRoom.RoomData.OwnerId);
                Response.AppendString(CurrentLoadingRoom.RoomData.Owner);
            }
            else
            {
                Response.AppendInteger(1);
                Response.AppendInteger(CurrentLoadingRoom.RoomData.OwnerId);
                Response.AppendString(CurrentLoadingRoom.RoomData.Owner);
            }

            Response.AppendInteger(array2.Length);

            RoomItem[] array4 = array2;

            foreach (RoomItem roomItem2 in array4)
                roomItem2.Serialize(Response);

            queuedServerMessage.AppendResponse(GetResponse());
            Array.Clear(array, 0, array.Length);
            Array.Clear(array2, 0, array2.Length);

            CurrentLoadingRoom.GetRoomUserManager().AddUserToRoom(Session, Session.GetHabbo().SpectatorMode);
            Session.GetHabbo().SpectatorMode = false;

            RoomCompetition competition = Yupi.GetGame().GetRoomManager().GetCompetitionManager().Competition;

            if (competition != null)
            {
                if (CurrentLoadingRoom.CheckRights(Session, true))
                {
                    if (!competition.Entries.ContainsKey(CurrentLoadingRoom.RoomData.Id))
                        competition.AppendEntrySubmitMessage(Response, CurrentLoadingRoom.RoomData.State != 0 ? 4 : 1);
                    else
                    {
                        switch (competition.Entries[CurrentLoadingRoom.RoomData.Id].CompetitionStatus)
                        {
                            case 3:
                                break;
                            default:
                                if (competition.HasAllRequiredFurnis(CurrentLoadingRoom))
                                    competition.AppendEntrySubmitMessage(Response, 2);
                                else
                                    competition.AppendEntrySubmitMessage(Response, 3, CurrentLoadingRoom);
                                break;
                        }
                    }
                }
                else if (!CurrentLoadingRoom.CheckRights(Session, true) &&
                         competition.Entries.ContainsKey(CurrentLoadingRoom.RoomData.Id))
                {
                    if (Session.GetHabbo().DailyCompetitionVotes > 0)
                        competition.AppendVoteMessage(Response, Session.GetHabbo());
                }

                queuedServerMessage.AppendResponse(GetResponse());
            }

            queuedServerMessage.SendResponse();

            if (Yupi.GetUnixTimeStamp() < Session.GetHabbo().FloodTime && Session.GetHabbo().FloodTime != 0)
            {
                ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("FloodFilterMessageComposer"));
                serverMessage.AppendInteger(Session.GetHabbo().FloodTime - Yupi.GetUnixTimeStamp());

                Session.SendMessage(serverMessage);
            }

            ClearRoomLoading();

            Poll poll;

            if (!Yupi.GetGame().GetPollManager().TryGetPoll(CurrentLoadingRoom.RoomId, out poll) ||
                Session.GetHabbo().GotPollData(poll.Id))
                return;

            Response.Init(LibraryParser.OutgoingRequest("SuggestPollMessageComposer"));
            poll.Serialize(Response);

            SendResponse();
        }

        internal void WidgetContainers()
        {
            string text = Request.GetString();

            if (Session == null)
                return;

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("LandingWidgetMessageComposer"));

            if (!string.IsNullOrEmpty(text))
            {
                string[] array = text.Split(',');

                serverMessage.AppendString(text);
                serverMessage.AppendString(array[1]);
            }
            else
            {
                serverMessage.AppendString(string.Empty);
                serverMessage.AppendString(string.Empty);
            }

            Session.SendMessage(serverMessage);
        }

        internal void RefreshPromoEvent()
        {
            HotelLandingManager hotelView = Yupi.GetGame().GetHotelView();

            if (Session?.GetHabbo() == null)
                return;

            if (hotelView.HotelViewPromosIndexers.Count <= 0)
                return;

            ServerMessage message =
                hotelView.SmallPromoComposer(
                    new ServerMessage(LibraryParser.OutgoingRequest("LandingPromosMessageComposer")));
            Session.SendMessage(message);
        }

        internal void AcceptPoll()
        {
            uint key = Request.GetUInteger();
            Poll poll = Yupi.GetGame().GetPollManager().Polls[key];

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PollQuestionsMessageComposer"));

            serverMessage.AppendInteger(poll.Id);
            serverMessage.AppendString(poll.PollName);
            serverMessage.AppendString(poll.Thanks);
            serverMessage.AppendInteger(poll.Questions.Count);

            foreach (PollQuestion current in poll.Questions)
            {
                int questionNumber = poll.Questions.IndexOf(current) + 1;

                current.Serialize(serverMessage, questionNumber);
            }

            Response = serverMessage;
            SendResponse();
        }

        internal void RefusePoll()
        {
            uint num = Request.GetUInteger();

            Session.GetHabbo().AnsweredPolls.Add(num);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("INSERT INTO users_polls VALUES (@userid , @pollid , 0 , '0' , '')");
                commitableQueryReactor.AddParameter("userid", Session.GetHabbo().Id);
                commitableQueryReactor.AddParameter("pollid", num);
                commitableQueryReactor.RunQuery();
            }
        }

        internal void AnswerPoll()
        {
            uint pollId = Request.GetUInteger();
            uint questionId = Request.GetUInteger();
            int num3 = Request.GetInteger();

            List<string> list = new List<string>();

            for (int i = 0; i < num3; i++)
                list.Add(Request.GetString());

            string text = string.Join("\r\n", list);

            Poll poll = Yupi.GetGame().GetPollManager().TryGetPollById(pollId);

            if (poll != null && poll.Type == PollType.Matching)
            {
                if (text == "1")
                    poll.AnswersPositive++;
                else
                    poll.AnswersNegative++;

                ServerMessage answered = new ServerMessage(LibraryParser.OutgoingRequest("MatchingPollAnsweredMessageComposer"));

                answered.AppendInteger(Session.GetHabbo().Id);
                answered.AppendString(text);
                answered.AppendInteger(0);
                Session.SendMessage(answered);
                Session.GetHabbo().AnsweredPool = true;

                return;
            }

            Session.GetHabbo().AnsweredPolls.Add(pollId);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "INSERT INTO users_polls VALUES (@userid , @pollid , @questionid , '1' , @answer)");

                commitableQueryReactor.AddParameter("userid", Session.GetHabbo().Id);
                commitableQueryReactor.AddParameter("pollid", pollId);
                commitableQueryReactor.AddParameter("questionid", questionId);
                commitableQueryReactor.AddParameter("answer", text);
                commitableQueryReactor.RunQuery();
            }
        }

        public string WallPositionCheck(string wallPosition)
        {
            try
            {
                if (wallPosition.Contains(Convert.ToChar(13)) || wallPosition.Contains(Convert.ToChar(9)))
                    return null;

                string[] array = wallPosition.Split(' ');

                if (array[2] != "l" && array[2] != "r")
                    return null;

                string[] array2 = array[0].Substring(3).Split(',');
                int num = int.Parse(array2[0]);
                int num2 = int.Parse(array2[1]);

                if (num >= 0 && num2 >= 0 && num <= 200 && num2 <= 200)
                {
                    string[] array3 = array[1].Substring(2).Split(',');
                    int num3 = int.Parse(array3[0]);
                    int num4 = int.Parse(array3[1]);

                    return num3 < 0 || num4 < 0 || num3 > 200 || num4 > 200
                        ? null
                        : string.Concat(":w=", num, ",", num2, " l=", num3, ",", num4, " ", array[2]);
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        internal void Sit()
        {
            RoomUser user = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (user == null)
                return;

            if (user.Statusses.ContainsKey("lay") || user.IsLyingDown || user.RidingHorse || user.IsWalking)
                return;

            if (user.RotBody%2 != 0)
                user.RotBody--;

            user.Z = Session.GetHabbo().CurrentRoom.GetGameMap().SqAbsoluteHeight(user.X, user.Y);

            if (!user.Statusses.ContainsKey("sit"))
            {
                user.UpdateNeeded = true;
                user.Statusses.Add("sit", "0.55");
            }

            user.IsSitting = true;
        }

        public void Whisper()
        {
            if (!Session.GetHabbo().InRoom)
                return;

            Room currentRoom = Session.GetHabbo().CurrentRoom;
            string text = Request.GetString();
            string text2 = text.Split(' ')[0];
            string msg = text.Substring(text2.Length + 1);
            int colour = Request.GetInteger();

            RoomUser roomUserByHabbo = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            RoomUser roomUserByHabbo2 = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(text2);

            msg = currentRoom.WordFilter.Aggregate(msg,
                (current1, current) => Regex.Replace(current1, current, "bobba", RegexOptions.IgnoreCase));

            BlackWord word;

            if (BlackWordsManager.Check(msg, BlackWordType.Hotel, out word))
            {
                BlackWordTypeSettings settings = word.TypeSettings;

                if (settings.ShowMessage)
                {
                    Session.SendWhisper("A mensagem enviada tem a palavra: " + word.Word +
                                        " Que não é permitida aqui, você poderá ser banido!");
                    return;
                }
            }

            TimeSpan span = DateTime.Now - _floodTime;

            if (span.Seconds > 4)
                _floodCount = 0;

            if ((span.Seconds < 4) && (_floodCount > 5) && (Session.GetHabbo().Rank < 5))
                return;

            _floodTime = DateTime.Now;
            _floodCount++;

            if (roomUserByHabbo == null || roomUserByHabbo2 == null)
            {
                Session.SendWhisper(msg);
                return;
            }

            if (Session.GetHabbo().Rank < 4 && currentRoom.CheckMute(Session))
                return;

            currentRoom.AddChatlog(Session.GetHabbo().Id, $"<whispered to {text2}>: {msg}", false);

            int colour2 = colour;

            if (!roomUserByHabbo.IsBot &&
                (colour2 == 2 || (colour2 == 23 && !Session.GetHabbo().HasFuse("fuse_mod")) || colour2 < 0 ||
                 colour2 > 29))
                colour2 = roomUserByHabbo.LastBubble; // or can also be just 0

            roomUserByHabbo.UnIdle();

            ServerMessage whisp = new ServerMessage(LibraryParser.OutgoingRequest("WhisperMessageComposer"));
            whisp.AppendInteger(roomUserByHabbo.VirtualId);
            whisp.AppendString(msg);
            whisp.AppendInteger(0);
            whisp.AppendInteger(colour2);
            whisp.AppendInteger(0);
            whisp.AppendInteger(-1);

            roomUserByHabbo.GetClient().SendMessage(whisp);

            if (!roomUserByHabbo2.IsBot && roomUserByHabbo2.UserId != roomUserByHabbo.UserId &&
                !roomUserByHabbo2.GetClient().GetHabbo().MutedUsers.Contains(Session.GetHabbo().Id))
                roomUserByHabbo2.GetClient().SendMessage(whisp);

            List<RoomUser> roomUserByRank = currentRoom.GetRoomUserManager().GetRoomUserByRank(4);

            if (!roomUserByRank.Any())
                return;

            foreach (RoomUser current2 in roomUserByRank)
                if (current2 != null && current2.HabboId != roomUserByHabbo2.HabboId &&
                    current2.HabboId != roomUserByHabbo.HabboId && current2.GetClient() != null)
                {
                    ServerMessage whispStaff = new ServerMessage(LibraryParser.OutgoingRequest("WhisperMessageComposer"));

                    whispStaff.AppendInteger(roomUserByHabbo.VirtualId);
                    whispStaff.AppendString($"Whisper to {text2}: {msg}");
                    whispStaff.AppendInteger(0);
                    whispStaff.AppendInteger(colour2);
                    whispStaff.AppendInteger(0);
                    whispStaff.AppendInteger(-1);

                    current2.GetClient().SendMessage(whispStaff);
                }
        }

        public void Chat()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            RoomUser roomUser = room?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUser == null)
                return;

            string message = Request.GetString();
            int bubble = Request.GetInteger();
            int count = Request.GetInteger();

            if (!roomUser.IsBot)
                if (bubble == 2 || (bubble == 23 && !Session.GetHabbo().HasFuse("fuse_mod")) || bubble < 0 ||
                    bubble > 29)
                    bubble = roomUser.LastBubble;

            roomUser.Chat(Session, message, false, count, bubble);
        }

        public void Shout()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            string msg = Request.GetString();
            int bubble = Request.GetInteger();

            if (!roomUserByHabbo.IsBot)
                if (bubble == 2 || (bubble == 23 && !Session.GetHabbo().HasFuse("fuse_mod")) || bubble < 0 ||
                    bubble > 29)
                    bubble = roomUserByHabbo.LastBubble;

            roomUserByHabbo.Chat(Session, msg, true, -1, bubble);
        }

        public void GetFloorPlanUsedCoords()
        {
            Response.Init(LibraryParser.OutgoingRequest("GetFloorPlanUsedCoordsMessageComposer"));

            Room room = Session.GetHabbo().CurrentRoom;

            if (room == null)
                Response.AppendInteger(0);
            else
            {
                Point[] coords = room.GetGameMap().CoordinatedItems.Keys.OfType<Point>().ToArray();

                Response.AppendInteger(coords.Count());

                foreach (Point point in coords)
                {
                    Response.AppendInteger(point.X);
                    Response.AppendInteger(point.Y);
                }
            }

            SendResponse();
        }

        public void GetFloorPlanDoor()
        {
            Room room = Session.GetHabbo().CurrentRoom;

            if (room == null)
                return;

            Response.Init(LibraryParser.OutgoingRequest("SetFloorPlanDoorMessageComposer"));
            Response.AppendInteger(room.GetGameMap().Model.DoorX);
            Response.AppendInteger(room.GetGameMap().Model.DoorY);
            Response.AppendInteger(room.GetGameMap().Model.DoorOrientation);

            SendResponse();
        }

        public Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);

            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        public void EnterRoomQueue()
        {
            Session.SendNotif("Currently working on Watch live TV");

            Session.GetHabbo().SpectatorMode = true;

            ServerMessage forwardToRoom = new ServerMessage(LibraryParser.OutgoingRequest("RoomForwardMessageComposer"));
            forwardToRoom.AppendInteger(1);

            Session.SendMessage(forwardToRoom);
        }

        public void GetCameraRequest()
        {
            try
            {
                int count = Request.GetInteger();
                byte[] bytes = Request.GetBytes(count);
                string outData = Converter.Deflate(bytes);

                string url = WebManager.HttpPostJson(ServerExtraSettings.StoriesApiServerUrl, outData);
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                dynamic jsonArray = serializer.Deserialize<object>(outData);
                string encodedurl = ServerExtraSettings.StoriesApiHost + url;
                encodedurl = encodedurl.Replace("\n", string.Empty);

                int roomId = jsonArray["roomid"];
                long timeStamp = jsonArray["timestamp"];

                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    commitableQueryReactor.SetQuery(
                        "INSERT INTO cms_stories_photos_preview (user_id,user_name,room_id,image_preview_url,image_url,type,date,tags) VALUES (@userid,@username,@roomid,@imagepreviewurl,@imageurl,@types,@dates,@tag)");
                    commitableQueryReactor.AddParameter("userid", Session.GetHabbo().Id);
                    commitableQueryReactor.AddParameter("username", Session.GetHabbo().UserName);
                    commitableQueryReactor.AddParameter("roomid", roomId);
                    commitableQueryReactor.AddParameter("imagepreviewurl", encodedurl);
                    commitableQueryReactor.AddParameter("imageurl", encodedurl);
                    commitableQueryReactor.AddParameter("types", "PHOTO");
                    commitableQueryReactor.AddParameter("dates", timeStamp);
                    commitableQueryReactor.AddParameter("tag", "");
                    commitableQueryReactor.RunQuery();
                }

                ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("CameraStorageUrlMessageComposer"));
                message.AppendString(url);

                Session.SendMessage(message);
            }
            catch (Exception)
            {
                Session.SendNotif("Essa foto possui muitos itens, por favor tire foto de um lugar com menos itens");
            }
        }

        public void SubmitRoomToCompetition()
        {
            Request.GetString();

            int code = Request.GetInteger();
            Room room = Session.GetHabbo().CurrentRoom;
            RoomData roomData = room?.RoomData;

            if (roomData == null)
                return;

            RoomCompetition competition = Yupi.GetGame().GetRoomManager().GetCompetitionManager().Competition;

            if (competition == null)
                return;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                if (code == 2)
                {
                    if (competition.Entries.ContainsKey(room.RoomId))
                        return;

                    commitableQueryReactor.SetQuery(
                        "INSERT INTO rooms_competitions_entries (competition_id, room_id, status) VALUES (@competition_id, @room_id, @status)");

                    commitableQueryReactor.AddParameter("competition_id", competition.Id);
                    commitableQueryReactor.AddParameter("room_id", room.RoomId);
                    commitableQueryReactor.AddParameter("status", 2);
                    commitableQueryReactor.RunQuery();
                    competition.Entries.Add(room.RoomId, roomData);

                    ServerMessage message = new ServerMessage();

                    roomData.CompetitionStatus = 2;
                    competition.AppendEntrySubmitMessage(message, 3, room);

                    Session.SendMessage(message);
                }
                else if (code == 3)
                {
                    if (!competition.Entries.ContainsKey(room.RoomId))
                        return;

                    RoomData entry = competition.Entries[room.RoomId];

                    if (entry == null)
                        return;

                    commitableQueryReactor.SetQuery(
                        "UPDATE rooms_competitions_entries SET status = @status WHERE competition_id = @competition_id AND room_id = @roomid");

                    commitableQueryReactor.AddParameter("status", 3);
                    commitableQueryReactor.AddParameter("competition_id", competition.Id);
                    commitableQueryReactor.AddParameter("roomid", room.RoomId);
                    commitableQueryReactor.RunQuery();
                    roomData.CompetitionStatus = 3;

                    ServerMessage message = new ServerMessage();
                    competition.AppendEntrySubmitMessage(message, 0);

                    Session.SendMessage(message);
                }
            }
        }

        public void VoteForRoom()
        {
            Request.GetString();

            if (Session.GetHabbo().DailyCompetitionVotes <= 0)
                return;

            Room room = Session.GetHabbo().CurrentRoom;

            RoomData roomData = room?.RoomData;

            if (roomData == null)
                return;

            RoomCompetition competition = Yupi.GetGame().GetRoomManager().GetCompetitionManager().Competition;

            if (competition == null)
                return;

            if (!competition.Entries.ContainsKey(room.RoomId))
                return;

            RoomData entry = competition.Entries[room.RoomId];

            entry.CompetitionVotes++;
            Session.GetHabbo().DailyCompetitionVotes--;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "UPDATE rooms_competitions_entries SET votes = @votes WHERE competition_id = @competition_id AND room_id = @roomid");

                commitableQueryReactor.AddParameter("votes", entry.CompetitionVotes);
                commitableQueryReactor.AddParameter("competition_id", competition.Id);
                commitableQueryReactor.AddParameter("roomid", room.RoomId);
                commitableQueryReactor.RunQuery();
                commitableQueryReactor.RunFastQuery("UPDATE users_stats SET daily_competition_votes = " +
                                                    Session.GetHabbo().DailyCompetitionVotes + " WHERE id = " +
                                                    Session.GetHabbo().Id);
            }

            ServerMessage message = new ServerMessage();
            competition.AppendVoteMessage(message, Session.GetHabbo());

            Session.SendMessage(message);
        }
    }
}