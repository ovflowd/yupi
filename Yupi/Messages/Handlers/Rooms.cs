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
using Yupi.Game.Catalogs;
using Yupi.Game.Catalogs.Composers;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pathfinding;
using Yupi.Game.Pets;
using Yupi.Game.Pets.Enums;
using Yupi.Game.Polls;
using Yupi.Game.Polls.Enums;
using Yupi.Game.Quests;
using Yupi.Game.RoomBots;
using Yupi.Game.RoomBots.Enumerators;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Rooms.User;
using Yupi.Messages.Parsers;
using Yupi.Net.Web;

namespace Yupi.Messages.Handlers
{
    partial class GameClientMessageHandler
    {
        private int _floodCount;
        private DateTime _floodTime;

        public void GetPetBreeds()
        {
            var type = Request.GetString();
            string petType;
            var petId = PetRace.GetPetId(type, out petType);
            var races = PetRace.GetRacesForRaceId(petId);
            var message = new ServerMessage(LibraryParser.OutgoingRequest("SellablePetBreedsMessageComposer"));
            message.AppendString(petType);
            message.AppendInteger(races.Count);
            foreach (var current in races)
            {
                message.AppendInteger(petId);
                message.AppendInteger(current.Color1);
                message.AppendInteger(current.Color2);
                message.AppendBool(current.Has1Color);
                message.AppendBool(current.Has2Color);
            }
            Session.SendMessage(message);
        }

        internal void GoRoom()
        {
            if (Yupi.ShutdownStarted || Session == null || Session.GetHabbo() == null)
                return;
            var num = Request.GetUInteger();
            var roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(num);
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

            var roomId = Request.GetUInteger();

            GetResponse().Init(LibraryParser.OutgoingRequest("FavouriteRoomsUpdateMessageComposer"));
            GetResponse().AppendInteger(roomId);
            GetResponse().AppendBool(true);
            SendResponse();

            Session.GetHabbo().FavoriteRooms.Add(roomId);
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery("INSERT INTO users_favorites (user_id,room_id) VALUES (" + Session.GetHabbo().Id + "," + roomId + ")");
        }

        internal void RemoveFavorite()
        {
            if (Session.GetHabbo() == null)
                return;
            var roomId = Request.GetUInteger();
            Session.GetHabbo().FavoriteRooms.Remove(roomId);

            GetResponse().Init(LibraryParser.OutgoingRequest("FavouriteRoomsUpdateMessageComposer"));
            GetResponse().AppendInteger(roomId);
            GetResponse().AppendBool(false);
            SendResponse();

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery("DELETE FROM users_favorites WHERE user_id = " + Session.GetHabbo().Id + " AND room_id = " + roomId);
        }

        internal void OnlineConfirmationEvent()
        {
            Writer.WriteLine(Request.GetString() + " connected in the game. with ip " + Session.GetConnection().GetIp(), "Yupi.Users",
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
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room != null)
                room.GetRoomUserManager().RemoveUserFromRoom(Session, true, false);

            var hotelView = Yupi.GetGame().GetHotelView();
            if (hotelView.FurniRewardName != null)
            {
                var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("LandingRewardMessageComposer"));
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
            var onlineFriends = Session.GetHabbo().GetMessenger().Friends.Count(x => x.Value.IsOnline);
            var goalMeter = new ServerMessage(LibraryParser.OutgoingRequest("LandingCommunityChallengeMessageComposer"));
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
            var itemId = Request.GetUInteger();
            var count = Request.GetUInteger();

            if (Session == null || Session.GetHabbo() == null) return;
            var room = Session.GetHabbo().CurrentRoom;
            if (room == null || !room.CheckRights(Session, true)) return;

            var item = room.GetRoomItemHandler().GetItem(itemId);
            if (item == null)
                return;

            var extraData = string.Format("state{0}0", Convert.ToChar(9));
            for (uint i = 1; i <= count; i++)
                extraData = string.Format("{0}{1}{2}", extraData, Convert.ToChar(9), Request.GetString());

            item.ExtraData = extraData;
            room.GetRoomItemHandler()
                .SetFloorItem(Session, item, item.X, item.Y, item.Rot, false, false, true);
        }

        internal void OnRoomUserAdd()
        {
            if (Session == null || GetResponse() == null)
                return;
            var queuedServerMessage = new QueuedServerMessage(Session.GetConnection());
            if (CurrentLoadingRoom == null || CurrentLoadingRoom.GetRoomUserManager() == null ||
                CurrentLoadingRoom.GetRoomUserManager().UserList == null)
                return;
            var list =
                CurrentLoadingRoom.GetRoomUserManager()
                    .UserList.Values.Where(current => current != null && !current.IsSpectator);
            Response.Init(LibraryParser.OutgoingRequest("SetRoomUserMessageComposer"));
            Response.StartArray();
            foreach (var current2 in list)
            {
                try
                {
                    current2.Serialize(Response, CurrentLoadingRoom.GetGameMap().GotPublicPool);
                    Response.SaveArray();
                }
                catch (Exception e)
                {
                    Writer.LogException(e.ToString());
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

            foreach (var habboForId in CurrentLoadingRoom.UsersWithRights.Select(Yupi.GetHabboById))
            {
                if (habboForId == null) continue;

                GetResponse().Init(LibraryParser.OutgoingRequest("GiveRoomRightsMessageComposer"));
                GetResponse().AppendInteger(CurrentLoadingRoom.RoomId);
                GetResponse().AppendInteger(habboForId.Id);
                GetResponse().AppendString(habboForId.UserName);
                queuedServerMessage.AppendResponse(GetResponse());
            }

            var serverMessage = CurrentLoadingRoom.GetRoomUserManager().SerializeStatusUpdates(true);
            if (serverMessage != null)
                queuedServerMessage.AppendResponse(serverMessage);

            if (CurrentLoadingRoom.RoomData.Event != null)
                Yupi.GetGame().GetRoomEvents().SerializeEventInfo(CurrentLoadingRoom.RoomId);

            CurrentLoadingRoom.JustLoaded = false;
            foreach (var current4 in CurrentLoadingRoom.GetRoomUserManager().UserList.Values.Where(current4 => current4 != null))
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
                    var sleepMsg = new ServerMessage(LibraryParser.OutgoingRequest("RoomUserIdleMessageComposer"));
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
                        var serverMessage2 =
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
                    ServerLogManager.HandleException(pException, "Rooms.SendRoomData3");
                }
            }
            queuedServerMessage.SendResponse();
        }

        internal void EnterOnRoom()
        {
            if (Yupi.ShutdownStarted) return;

            var id = Request.GetUInteger();
            var password = Request.GetString();
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
                var queuedServerMessage = new QueuedServerMessage(Session.GetConnection());

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
                    Session.GetHabbo().Id != (ulong)room.RoomData.OwnerId)
                {
                    var roomQueue = new ServerMessage(LibraryParser.OutgoingRequest("RoomsQueue"));

                    roomQueue.AppendInteger(2);
                    roomQueue.AppendString("visitors");
                    roomQueue.AppendInteger(2);
                    roomQueue.AppendInteger(1);
                    roomQueue.AppendString("visitors");
                    roomQueue.AppendInteger(room.UserCount - (int)room.RoomData.UsersNow); // Currently people are in the queue -1 ()
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

                        var serverMessage2 =
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
                if (!isReload && !Session.GetHabbo().HasFuse("fuse_enter_any_room") && !room.CheckRightsDoorBell(Session, true, true, room.RoomData.Group != null ? room.RoomData.Group.Members.ContainsKey(Session.GetHabbo().Id) : false) &&
                  !(Session.GetHabbo().IsTeleporting && Session.GetHabbo().TeleportingRoomId == id) && !Session.GetHabbo().IsHopping)
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
                            var serverMessage3 =
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
                Writer.LogException("PrepareRoomForUser. RoomId: " + id + "; UserId: " +
                                           (Session != null
                                               ? Session.GetHabbo().Id.ToString(CultureInfo.InvariantCulture)
                                               : "null") + Environment.NewLine + e);
            }
        }

        internal void ReqLoadRoomForUser()
        {
            LoadRoomForUser().SendResponse();
        }

        internal QueuedServerMessage LoadRoomForUser()
        {
            var currentLoadingRoom = CurrentLoadingRoom;
            var queuedServerMessage = new QueuedServerMessage(Session.GetConnection());
            if (currentLoadingRoom == null || !Session.GetHabbo().LoadingChecksPassed) return queuedServerMessage;
            if (Session.GetHabbo().FavouriteGroup > 0u)
            {
                if (CurrentLoadingRoom.RoomData.Group != null &&
                    !CurrentLoadingRoom.LoadedGroups.ContainsKey((uint) CurrentLoadingRoom.RoomData.Group.Id))
                    CurrentLoadingRoom.LoadedGroups.Add((uint) CurrentLoadingRoom.RoomData.Group.Id,
                        CurrentLoadingRoom.RoomData.Group.Badge);
                if (!CurrentLoadingRoom.LoadedGroups.ContainsKey((uint) Session.GetHabbo().FavouriteGroup) &&
                    Yupi.GetGame().GetGroupManager().GetGroup(Session.GetHabbo().FavouriteGroup) != null)
                    CurrentLoadingRoom.LoadedGroups.Add((uint) Session.GetHabbo().FavouriteGroup,
                        Yupi.GetGame().GetGroupManager().GetGroup(Session.GetHabbo().FavouriteGroup).Badge);
            }
            Response.Init(LibraryParser.OutgoingRequest("RoomGroupMessageComposer"));
            Response.AppendInteger(CurrentLoadingRoom.LoadedGroups.Count);
            foreach (var guild1 in CurrentLoadingRoom.LoadedGroups)
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

            RoomUser roomUserByVirtualId = currentRoom.GetRoomUserManager().GetRoomUserByVirtualId((int)(roomUserByHabbo.HorseId));

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
            if ((Yupi.GetUnixTimeStamp() - Session.GetHabbo().LastSqlQuery) < 20)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("user_create_room_flood_error"));
                return;
            }

            var name = Request.GetString();
            var description = Request.GetString();
            var roomModel = Request.GetString();
            var category = Request.GetInteger();
            var maxVisitors = Request.GetInteger();
            var tradeState = Request.GetInteger();

            var data = Yupi.GetGame()
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
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Convert.ToUInt32(Request.GetInteger()));
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
                .AppendInteger(((room.RoomData.Model.MapSizeX * room.RoomData.Model.MapSizeY) > 200) ? 50 : 25);

            GetResponse().AppendInteger(room.TagCount);
            foreach (var s in room.RoomData.Tags)
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
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RoomFloorWallLevelsMessageComposer"));
            serverMessage.AppendBool(room.RoomData.HideWall);
            serverMessage.AppendInteger(room.RoomData.WallThickness);
            serverMessage.AppendInteger(room.RoomData.FloorThickness);
            return serverMessage;
        }

        internal static ServerMessage SerializeRoomChatOption(Room room)
        {
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RoomChatOptionsMessageComposer"));
            serverMessage.AppendInteger(room.RoomData.ChatType);
            serverMessage.AppendInteger(room.RoomData.ChatBalloon);
            serverMessage.AppendInteger(room.RoomData.ChatSpeed);
            serverMessage.AppendInteger(room.RoomData.ChatMaxDistance);
            serverMessage.AppendInteger(room.RoomData.ChatFloodProtection);
            return serverMessage;
        }

        internal void ParseRoomDataInformation()
        {
            var id = Request.GetUInteger();
            var num = Request.GetInteger();
            var num2 = Request.GetInteger();
            var room = Yupi.GetGame().GetRoomManager().LoadRoom(id);
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
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(string.Format("SELECT user_id FROM rooms_rights WHERE room_id={0}",
                    room.RoomId));
                table = queryReactor.GetTable();
            }
            Response.Init(LibraryParser.OutgoingRequest("LoadRoomRightsListMessageComposer"));
            GetResponse().AppendInteger(room.RoomData.Id);
            GetResponse().AppendInteger(table.Rows.Count);

            foreach (var habboForId in table.Rows.Cast<DataRow>().Select(dataRow => Yupi.GetHabboById((uint)dataRow[0])).Where(habboForId => habboForId != null))
            {
                GetResponse().AppendInteger(habboForId.Id);
                GetResponse().AppendString(habboForId.UserName);
            }
            SendResponse();
        }

        internal void SaveRoomData()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            Request.GetInteger();

            var oldName = room.RoomData.Name;
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
            var tagCount = Request.GetUInteger();

            if (tagCount > 2) return;
            var tags = new List<string>();

            for (var i = 0; i < tagCount; i++)
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
            var flatCat = Yupi.GetGame().GetNavigator().GetFlatCat(room.RoomData.Category);
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
            var num = Request.GetUInteger();
            var room = Yupi.GetGame().GetRoomManager().GetRoom(num);
            if (room == null)
                return;
            var list = room.BannedUsers();
            Response.Init(LibraryParser.OutgoingRequest("RoomBannedListMessageComposer"));
            Response.AppendInteger(num);
            Response.AppendInteger(list.Count);
            foreach (var current in list)
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
            foreach (var current in Session.GetHabbo().CurrentRoom.UsersWithRights)
            {
                var habboForId = Yupi.GetHabboById(current);
                Response.AppendInteger(current);
                Response.AppendString((habboForId == null) ? "Undefined" : habboForId.UserName);
            }
            SendResponse();
        }

        internal void UnbanUser()
        {
            var num = Request.GetUInteger();
            var num2 = Request.GetUInteger();
            var room = Yupi.GetGame().GetRoomManager().GetRoom(num2);
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
            var num = Request.GetUInteger();
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(num);
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
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("INSERT INTO rooms_rights (room_id,user_id) VALUES (", room.RoomId, ",", num, ")"));
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
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            var stringBuilder = new StringBuilder();
            var num = Request.GetInteger();

            {
                for (var i = 0; i < num; i++)
                {
                    if (i > 0)
                        stringBuilder.Append(" OR ");
                    var num2 = Request.GetUInteger();
                    if (room.UsersWithRights.Contains(num2))
                        room.UsersWithRights.Remove(num2);
                    stringBuilder.Append(string.Concat("room_id = '", room.RoomId, "' AND user_id = '", num2, "'"));
                    var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(num2);
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
                using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery(string.Format("DELETE FROM rooms_rights WHERE {0}", stringBuilder));
            }
        }

        internal void TakeAllRights()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            DataTable table;
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(string.Format("SELECT user_id FROM rooms_rights WHERE room_id={0}", room.RoomId));
                table = queryReactor.GetTable();
            }
            foreach (DataRow dataRow in table.Rows)
            {
                var num = (uint)dataRow[0];
                var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(num);
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
            using (var queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                queryreactor2.RunFastQuery(string.Format("DELETE FROM rooms_rights WHERE room_id = {0}", room.RoomId));
            room.UsersWithRights.Clear();
            UsersWithRights();
        }

        internal void KickUser()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;

            if (!room.CheckRights(Session) && room.RoomData.WhoCanKick != 2 && Session.GetHabbo().Rank < Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]))
                return;

            var pId = Request.GetUInteger();
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(pId);
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
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || (room.RoomData.WhoCanBan == 0 && !room.CheckRights(Session, true)) ||
                (room.RoomData.WhoCanBan == 1 && !room.CheckRights(Session)))
                return;
            var num = Request.GetInteger();
            Request.GetUInteger();
            var text = Request.GetString();
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Convert.ToUInt32(num));
            if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
                return;
            if (roomUserByHabbo.GetClient().GetHabbo().HasFuse("fuse_mod") ||
                roomUserByHabbo.GetClient().GetHabbo().HasFuse("fuse_no_kick"))
                return;
            var time = 0L;
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
                using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery(string.Concat("UPDATE users SET home_room = ", roomId, " WHERE id = ", Session.GetHabbo().Id));

                Response.Init(LibraryParser.OutgoingRequest("HomeRoomMessageComposer"));
                Response.AppendInteger(roomId);
                Response.AppendInteger(0);
                SendResponse();
            }
        }

        internal void DeleteRoom()
        {
            var roomId = Request.GetUInteger();
            if (Session == null || Session.GetHabbo() == null || Session.GetHabbo().UsersRooms == null)
                return;
            var room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);
            if (room == null)
                return;
            if (room.RoomData.Owner != Session.GetHabbo().UserName && Session.GetHabbo().Rank <= 6u)
                return;
            if (Session.GetHabbo().GetInventoryComponent() != null)
                Session.GetHabbo()
                    .GetInventoryComponent()
                    .AddItemArray(room.GetRoomItemHandler().RemoveAllFurniture(Session));
            var roomData = room.RoomData;
            Yupi.GetGame().GetRoomManager().UnloadRoom(room, "Delete room");
            Yupi.GetGame().GetRoomManager().QueueVoteRemove(roomData);
            if (roomData == null || Session == null)
                return;
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery(string.Format("DELETE FROM rooms_data WHERE id = {0}", roomId));
                queryReactor.RunFastQuery(string.Format("DELETE FROM users_favorites WHERE room_id = {0}", roomId));
                queryReactor.RunFastQuery(string.Format("DELETE FROM items_rooms WHERE room_id = {0}", roomId));
                queryReactor.RunFastQuery(string.Format("DELETE FROM rooms_rights WHERE room_id = {0}", roomId));
                queryReactor.RunFastQuery(string.Format("UPDATE users SET home_room = '0' WHERE home_room = {0}",
                    roomId));
            }
            if (Session.GetHabbo().Rank > 5u && Session.GetHabbo().UserName != roomData.Owner)
                Yupi.GetGame()
                    .GetModerationTool()
                    .LogStaffEntry(Session.GetHabbo().UserName, roomData.Name, "Room deletion",
                        string.Format("Deleted room ID {0}", roomData.Id));
            var roomData2 = (
                from p in Session.GetHabbo().UsersRooms
                where p.Id == roomId
                select p).SingleOrDefault();
            if (roomData2 != null)
                Session.GetHabbo().UsersRooms.Remove(roomData2);
        }

        internal void LookAt()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            roomUserByHabbo.UnIdle();
            var x = Request.GetInteger();
            var y = Request.GetInteger();
            if (x == roomUserByHabbo.X && y == roomUserByHabbo.Y)
                return;
            var rotation = PathFinder.CalculateRotation(roomUserByHabbo.X, roomUserByHabbo.Y, x, y);
            roomUserByHabbo.SetRot(rotation, false);
            roomUserByHabbo.UpdateNeeded = true;

            if (!roomUserByHabbo.RidingHorse)
                return;
            var roomUserByVirtualId =
                Session.GetHabbo()
                    .CurrentRoom.GetRoomUserManager()
                    .GetRoomUserByVirtualId(Convert.ToInt32(roomUserByHabbo.HorseId));
            roomUserByVirtualId.SetRot(rotation, false);
            roomUserByVirtualId.UpdateNeeded = true;
        }

        internal void StartTyping()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("TypingStatusMessageComposer"));
            serverMessage.AppendInteger(roomUserByHabbo.VirtualId);
            serverMessage.AppendInteger(1);
            room.SendMessage(serverMessage);
        }

        internal void StopTyping()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("TypingStatusMessageComposer"));
            serverMessage.AppendInteger(roomUserByHabbo.VirtualId);
            serverMessage.AppendInteger(0);
            room.SendMessage(serverMessage);
        }

        internal void IgnoreUser()
        {
            if (Session.GetHabbo().CurrentRoom == null)
                return;
            var text = Request.GetString();
            var habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(text).GetHabbo();
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
            var text = Request.GetString();
            var habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(text).GetHabbo();
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
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            var b = true;
            var i = 0;
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
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            roomUserByHabbo.UnIdle();
            var value = Request.GetInteger();
            roomUserByHabbo.AddStatus("sign", Convert.ToString(value));
            roomUserByHabbo.UpdateNeeded = true;
            roomUserByHabbo.SignTime = (Yupi.GetUnixTimeStamp() + 5);
        }

        internal void InitRoomGroupBadges()
        {
            Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().LoadingRoom);
        }

        internal void RateRoom()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
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
                using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery(string.Concat("UPDATE rooms_data SET score = ", room.RoomData.Score, " WHERE id = ", room.RoomId));
                Session.GetHabbo().RatedRooms.Add(room.RoomId);
                Response.Init(LibraryParser.OutgoingRequest("RoomRatingMessageComposer"));
                Response.AppendInteger(room.RoomData.Score);
                Response.AppendBool(room.CheckRights(Session, true));
                SendResponse();
            }
        }

        internal void Dance()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            roomUserByHabbo.UnIdle();
            var num = Request.GetInteger();
            if (num < 0 || num > 4)
                num = 0;
            if (num > 0 && roomUserByHabbo.CarryItemId > 0)
                roomUserByHabbo.CarryItem(0);
            roomUserByHabbo.DanceId = num;
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("DanceStatusMessageComposer"));
            serverMessage.AppendInteger(roomUserByHabbo.VirtualId);
            serverMessage.AppendInteger(num);
            room.SendMessage(serverMessage);
            Yupi.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.SocialDance);
            if (room.GetRoomUserManager().GetRoomUsers().Count > 19)
                Yupi.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.MassDance);
        }

        internal void AnswerDoorbell()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session))
                return;
            var userName = Request.GetString();
            var flag = Request.GetBool();
            var clientByUserName = Yupi.GetGame().GetClientManager().GetClientByUserName(userName);
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
            var num = Request.GetUInteger();
            var flag = Request.GetBool();
            var text = Request.GetString();
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            if (!flag)
            {
                if (!room.WordFilter.Contains(text))
                    return;
                room.WordFilter.Remove(text);
                using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("DELETE FROM rooms_wordfilter WHERE room_id = @id AND word = @word");
                    queryReactor.AddParameter("id", num);
                    queryReactor.AddParameter("word", text);
                    queryReactor.RunQuery();
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
            using (var queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryreactor2.SetQuery("INSERT INTO rooms_wordfilter (room_id, word) VALUES (@id, @word);");
                queryreactor2.AddParameter("id", num);
                queryreactor2.AddParameter("word", text);
                queryreactor2.RunQuery();
            }
        }

        internal void GetRoomFilter()
        {
            var roomId = Request.GetUInteger();
            var room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            var serverMessage = new ServerMessage();
            serverMessage.Init(LibraryParser.OutgoingRequest("RoomLoadFilterMessageComposer"));
            serverMessage.AppendInteger(room.WordFilter.Count);
            foreach (var current in room.WordFilter)
                serverMessage.AppendString(current);
            Response = serverMessage;
            SendResponse();
        }

        internal void ApplyRoomEffect()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            var item = Session.GetHabbo().GetInventoryComponent().GetItem(Request.GetUInteger());
            if (item == null)
                return;
            var type = "floor";

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
                    Yupi.GetGame()
                        .GetQuestManager()
                        .ProgressUserQuest(Session, QuestType.FurniDecorationFloor);
                    break;

                case "wallpaper":

                    room.RoomData.WallPaper = item.ExtraData;

                    Yupi.GetGame()
                        .GetAchievementManager()
                        .ProgressUserAchievement(Session, "ACH_RoomDecoWallpaper", 1);
                    Yupi.GetGame()
                        .GetQuestManager()
                        .ProgressUserQuest(Session, QuestType.FurniDecorationWall);
                    break;

                case "landscape":

                    room.RoomData.LandScape = item.ExtraData;

                    Yupi.GetGame()
                        .GetAchievementManager()
                        .ProgressUserAchievement(Session, "ACH_RoomDecoLandscape", 1);
                    break;
            }
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(string.Concat("UPDATE rooms_data SET ", type, " = @extradata WHERE id = ", room.RoomId));
                queryReactor.AddParameter("extradata", item.ExtraData);
                queryReactor.RunQuery();
                queryReactor.RunFastQuery(string.Format("DELETE FROM items_rooms WHERE id={0} LIMIT 1", item.Id));
            }
            Session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RoomSpacesMessageComposer"));
            serverMessage.AppendString(type);
            serverMessage.AppendString(item.ExtraData);
            room.SendMessage(serverMessage);
        }

        internal void PromoteRoom()
        {
            var pageId = Request.GetInteger();
            var item = Request.GetUInteger();

            var page2 = Yupi.GetGame().GetCatalog().GetPage(pageId);
            if (page2 == null) return;
            var catalogItem = page2.GetItem(item);

            if (catalogItem == null) return;

            var num = Request.GetUInteger();
            var text = Request.GetString();
            Request.GetBool();

            var text2 = string.Empty;
            try
            {
                text2 = Request.GetString();
            }
            catch (Exception)
            {
            }
            var category = Request.GetInteger();

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(num) ?? new Room();
            room.Start(Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(num), true);

            if (!room.CheckRights(Session, true)) return;
            if (catalogItem.CreditsCost > 0)
            {
                if (catalogItem.CreditsCost > Session.GetHabbo().Credits) return;
                Session.GetHabbo().Credits -= (int)catalogItem.CreditsCost;
                Session.GetHabbo().UpdateCreditsBalance();
            }
            if (catalogItem.DucketsCost > 0)
            {
                if (catalogItem.DucketsCost > Session.GetHabbo().ActivityPoints) return;
                Session.GetHabbo().ActivityPoints -= (int)catalogItem.DucketsCost;
                Session.GetHabbo().UpdateActivityPointsBalance();
            }
            if (catalogItem.DiamondsCost > 0)
            {
                if (catalogItem.DiamondsCost > Session.GetHabbo().Diamonds) return;
                Session.GetHabbo().Diamonds -= (int)catalogItem.DiamondsCost;
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
            var serverMessage = new ServerMessage();
            serverMessage.Init(LibraryParser.OutgoingRequest("CatalogPromotionGetRoomsMessageComposer"));
            serverMessage.AppendBool(true);
            serverMessage.AppendInteger(Session.GetHabbo().UsersRooms.Count);
            foreach (var current in Session.GetHabbo().UsersRooms)
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
            if (Session != null && Session.GetHabbo() != null)
            {
                var room = Session.GetHabbo().CurrentRoom;

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

                var heightMap = Request.GetString();
                var doorX = Request.GetInteger();
                var doorY = Request.GetInteger();
                var doorOrientation = Request.GetInteger();
                var wallThickness = Request.GetInteger();
                var floorThickness = Request.GetInteger();
                var wallHeight = Request.GetInteger();

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
                    var message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
                    message.AppendString("floorplan_editor.error");
                    message.AppendInteger(1);
                    message.AppendString("errors");
                    message.AppendString(
                        "(general): too large height (max 64 tiles)\r(general): too large area (max 1800 tiles)");
                    Session.SendMessage(message);

                    return;
                }

                if (heightMap.Split((char)13).Length - 1 < doorY)
                {
                    var message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
                    message.AppendString("floorplan_editor.error");
                    message.AppendInteger(1);
                    message.AppendString("errors");
                    message.AppendString("Y: Door is in invalid place.");
                    Session.SendMessage(message);

                    return;
                }

                var lines = heightMap.Split((char)13);
                var lineWidth = lines[0].Length;
                for (var i = 1; i < lines.Length; i++)
                    if (lines[i].Length != lineWidth)
                    {
                        var message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
                        message.AppendString("floorplan_editor.error");
                        message.AppendInteger(1);
                        message.AppendString("errors");
                        message.AppendString("(general): Line " + (i + 1) + " is of different length than line 1");
                        Session.SendMessage(message);

                        return;
                    }
                var doorZ = 0.0;
                var charDoor = lines[doorY][doorX];
                if (charDoor >= (char)97 && charDoor <= 119) // a-w
                {
                    doorZ = charDoor - 87;
                }
                else
                {
                    double.TryParse(charDoor.ToString(), out doorZ);
                }
                using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("REPLACE INTO rooms_models_customs (roomid,door_x,door_y,door_z,door_dir,heightmap,poolmap) VALUES ('" + room.RoomId + "', '" + doorX + "','" +
                                      doorY + "','" + doorZ.ToString(CultureInfo.InvariantCulture).Replace(',', '.') + "','" + doorOrientation + "',@newmodel,'')");
                    queryReactor.AddParameter("newmodel", heightMap);
                    queryReactor.RunQuery();

                    room.RoomData.WallHeight = wallHeight;
                    room.RoomData.WallThickness = wallThickness;
                    room.RoomData.FloorThickness = floorThickness;
                    room.RoomData.Model.DoorZ = doorZ;

                    Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_RoomDecoHoleFurniCount", 1);

                    queryReactor.RunFastQuery(
                        string.Format(
                            "UPDATE rooms_data SET model_name = 'custom', wallthick = '{0}', floorthick = '{1}', walls_height = '{2}' WHERE id = {3};",
                            wallThickness, floorThickness, wallHeight, room.RoomId));
                    RoomModel roomModel = new RoomModel(doorX, doorY, doorZ, doorOrientation, heightMap, "", false, "");
                    Yupi.GetGame().GetRoomManager().UpdateCustomModel(room.RoomId, roomModel);
                    room.ResetGameMap("custom", wallHeight, wallThickness, floorThickness);
                    Yupi.GetGame().GetRoomManager().UnloadRoom(room, "Reload floor");

                    var forwardToRoom = new ServerMessage(LibraryParser.OutgoingRequest("RoomForwardMessageComposer"));
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

            if ((mopla.GetBaseItem().InteractionType != Interaction.Moplaseed) && (mopla.GetBaseItem().InteractionType != Interaction.RareMoplaSeed))
                return;
            if (string.IsNullOrEmpty(mopla.ExtraData) || mopla.ExtraData == "0")
                rarity = 1;
            if (!string.IsNullOrEmpty(mopla.ExtraData) && mopla.ExtraData != "0")
                rarity = int.TryParse(mopla.ExtraData, out internalRarity) ? internalRarity : 1;

            var getX = mopla.X;
            var getY = mopla.Y;
            room.GetRoomItemHandler().RemoveFurniture(Session, mopla.Id, false);
            var pet = CatalogManager.CreatePet(Session.GetHabbo().Id, "Monsterplant", 16, "0", "0", rarity);
            Response.Init(LibraryParser.OutgoingRequest("SendMonsterplantIdMessageComposer"));
            Response.AppendInteger(pet.PetId);
            SendResponse();
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("UPDATE bots_data SET room_id = '", room.RoomId, "', x = '", getX, "', y = '", getY, "' WHERE id = '", pet.PetId, "'"));
            pet.PlacedInRoom = true;
            pet.RoomId = room.RoomId;
            var bot = new RoomBot(pet.PetId, pet.OwnerId, pet.RoomId, AiType.Pet, "freeroam", pet.Name, "", pet.Look,
                getX, getY, 0.0, 4, null, null, "", 0, "");
            room.GetRoomUserManager().DeployBot(bot, pet);

            if (pet.DbState != DatabaseUpdateState.NeedsInsert)
                pet.DbState = DatabaseUpdateState.NeedsUpdate;

            using (var queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryreactor2.RunFastQuery(string.Format("DELETE FROM items_rooms WHERE id = {0}", mopla.Id));
                room.GetRoomUserManager().SavePets(queryreactor2);
            }
        }

        internal void KickBot()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            var roomUserByVirtualId = room.GetRoomUserManager().GetRoomUserByVirtualId(Request.GetInteger());
            if (roomUserByVirtualId == null || !roomUserByVirtualId.IsBot)
                return;

            room.GetRoomUserManager().RemoveBot(roomUserByVirtualId.VirtualId, true);
        }

        internal void PlacePet()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(Session, true)) ||
                !room.CheckRights(Session, true))
                return;

            var petId = Request.GetUInteger();
            var pet = Session.GetHabbo().GetInventoryComponent().GetPet(petId);

            if (pet == null || pet.PlacedInRoom)
                return;

            var x = Request.GetInteger();
            var y = Request.GetInteger();

            if (!room.GetGameMap().CanWalk(x, y, false))
                return;

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery("UPDATE bots_data SET room_id = '" + room.RoomId + "', x = '" + x + "', y = '" + y + "' WHERE id = '" + petId + "'");

            pet.PlacedInRoom = true;
            pet.RoomId = room.RoomId;

            room.GetRoomUserManager()
                .DeployBot(
                    new RoomBot(pet.PetId, Convert.ToUInt32(pet.OwnerId), pet.RoomId, AiType.Pet, "freeroam", pet.Name,
                        "", pet.Look, x, y, 0.0, 4, null, null, "", 0, ""), pet);
            Session.GetHabbo().GetInventoryComponent().MovePetToRoom(pet.PetId);
            if (pet.DbState != DatabaseUpdateState.NeedsInsert)
                pet.DbState = DatabaseUpdateState.NeedsUpdate;
            using (var queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                room.GetRoomUserManager().SavePets(queryreactor2);
            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializePetInventory());
        }

        internal void UpdateEventInfo()
        {
            Request.GetInteger();
            var original = Request.GetString();
            var original2 = Request.GetString();
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true) || room.RoomData.Event == null)
                return;
            room.RoomData.Event.Name = original;
            room.RoomData.Event.Description = original2;
            Yupi.GetGame().GetRoomEvents().UpdateEvent(room.RoomData.Event);
        }

        internal void HandleBotSpeechList()
        {
            var botId = Request.GetUInteger();
            var num2 = Request.GetInteger();
            var num3 = num2;

            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var bot = room.GetRoomUserManager().GetBot(botId);
            if (bot == null || !bot.IsBot)
                return;

            if (num3 == 2)
            {
                var text = bot.BotData.RandomSpeech == null ? string.Empty : string.Join("\n", bot.BotData.RandomSpeech);
                text += ";#;";
                text += bot.BotData.AutomaticChat ? "true" : "false";
                text += ";#;";
                text += bot.BotData.SpeechInterval;
                text += ";#;";
                text += bot.BotData.MixPhrases ? "true" : "false";

                var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("BotSpeechListMessageComposer"));
                serverMessage.AppendInteger(botId);
                serverMessage.AppendInteger(num2);
                serverMessage.AppendString(text);
                Response = serverMessage;
                SendResponse();
                return;
            }
            if (num3 != 5)
                return;

            var serverMessage2 = new ServerMessage(LibraryParser.OutgoingRequest("BotSpeechListMessageComposer"));
            serverMessage2.AppendInteger(botId);
            serverMessage2.AppendInteger(num2);
            serverMessage2.AppendString(bot.BotData.Name);

            Response = serverMessage2;
            SendResponse();
        }

        internal void ManageBotActions()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            var botId = Request.GetUInteger();
            var action = Request.GetInteger();
            var data = Yupi.FilterInjectionChars(Request.GetString());
            var bot = room.GetRoomUserManager().GetBot(botId);
            var flag = false;
            switch (action)
            {
                case 1:
                    bot.BotData.Look = Session.GetHabbo().Look;
                    goto IL_439;
                case 2:
                    try
                    {
                        var array = data.Split(new[] { ";#;" }, StringSplitOptions.None);

                        var speechsJunk =
                            array[0].Substring(0, array[0].Length > 1024 ? 1024 : array[0].Length)
                                .Split(Convert.ToChar(13));
                        var speak = array[1] == "true";
                        var speechDelay = int.Parse(array[2]);
                        var mix = array[3] == "true";
                        if (speechDelay < 7) speechDelay = 7;

                        var speechs =
                            speechsJunk.Where(
                                speech =>
                                    !string.IsNullOrEmpty(speech) &&
                                    (!speech.ToLower().Contains("update") || !speech.ToLower().Contains("set")))
                                .Aggregate(string.Empty,
                                    (current, speech) =>
                                        current +
                                        (ServerUserChatTextHandler.FilterHtml(speech, Session.GetHabbo().GotCommand("ha")) + ";"));
                        using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        {
                            queryReactor.SetQuery(
                                "UPDATE bots_data SET automatic_chat = @autochat, speaking_interval = @interval, mix_phrases = @mix_phrases, speech = @speech WHERE id = @botid");

                            queryReactor.AddParameter("autochat", speak ? "1" : "0");
                            queryReactor.AddParameter("interval", speechDelay);
                            queryReactor.AddParameter("mix_phrases", mix ? "1" : "0");
                            queryReactor.AddParameter("speech", speechs);
                            queryReactor.AddParameter("botid", botId);
                            queryReactor.RunQuery();
                        }
                        var randomSpeech = speechs.Split(';').ToList();

                        room.GetRoomUserManager()
                            .UpdateBot(bot.VirtualId, bot, bot.BotData.Name, bot.BotData.Motto, bot.BotData.Look,
                                bot.BotData.Gender, randomSpeech, null, speak, speechDelay, mix);
                        flag = true;
                        goto IL_439;
                    }
                    catch (Exception e)
                    {
                        Writer.LogException(e.ToString());
                        return;
                    }
                case 3:
                    bot.BotData.WalkingMode = bot.BotData.WalkingMode == "freeroam" ? "stand" : "freeroam";
                    using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery("UPDATE bots_data SET walk_mode = @walkmode WHERE id = @botid");
                        queryReactor.AddParameter("walkmode", bot.BotData.WalkingMode);
                        queryReactor.AddParameter("botid", botId);
                        queryReactor.RunQuery();
                    }
                    goto IL_439;
                case 4:
                    break;

                case 5:
                    var name = ServerUserChatTextHandler.FilterHtml(data, Session.GetHabbo().GotCommand("ha"));
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
                var random = new Random();
                bot.DanceId = random.Next(1, 4);
                bot.BotData.DanceId = bot.DanceId;
            }
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("DanceStatusMessageComposer"));
            serverMessage.AppendInteger(bot.VirtualId);
            serverMessage.AppendInteger(bot.BotData.DanceId);
            Session.GetHabbo().CurrentRoom.SendMessage(serverMessage);
            IL_439:
            if (!flag)
            {
                var serverMessage2 = new ServerMessage(LibraryParser.OutgoingRequest("SetRoomUserMessageComposer"));
                serverMessage2.AppendInteger(1);
                bot.Serialize(serverMessage2, room.GetGameMap().GotPublicPool);
                room.SendMessage(serverMessage2);
            }
        }

        internal void BotErrorComposer(int errorid)
        {
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("GeneralErrorHabboMessageComposer"));
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
            var currentRoom = Session.GetHabbo().CurrentRoom;
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
            var num = Request.GetUInteger();
            Session.GetHabbo().FavoriteRooms.Remove(num);
            Response.Init(LibraryParser.OutgoingRequest("FavouriteRoomsUpdateMessageComposer"));
            Response.AppendInteger(num);
            Response.AppendBool(false);
            SendResponse();

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("DELETE FROM users_favorites WHERE user_id = ", Session.GetHabbo().Id, " AND room_id = ", num));
        }

        internal void RoomUserAction()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            roomUserByHabbo.UnIdle();
            var num = Request.GetInteger();
            roomUserByHabbo.DanceId = 0;

            var action = new ServerMessage(LibraryParser.OutgoingRequest("RoomUserActionMessageComposer"));
            action.AppendInteger(roomUserByHabbo.VirtualId);
            action.AppendInteger(num);
            room.SendMessage(action);

            if (num == 5)
            {
                roomUserByHabbo.IsAsleep = true;
                var sleep = new ServerMessage(LibraryParser.OutgoingRequest("RoomUserIdleMessageComposer"));
                sleep.AppendInteger(roomUserByHabbo.VirtualId);
                sleep.AppendBool(roomUserByHabbo.IsAsleep);
                room.SendMessage(sleep);
            }
            Yupi.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.SocialWave);
        }

        internal void GetRoomData1()
        {
            /*this.Response.Init(StaticClientMessageHandler.OutgoingRequest("297"));//Not in release
            this.Response.AppendInt32(0);
            this.SendResponse();*/
        }

        internal void GetRoomData2()
        {
            try
            {
                if (Session != null && Session.GetConnection() != null)
                {
                    var queuedServerMessage = new QueuedServerMessage(Session.GetConnection());
                    if (Session.GetHabbo().LoadingRoom <= 0u || CurrentLoadingRoom == null)
                        return;
                    var roomData = CurrentLoadingRoom.RoomData;
                    if (roomData == null)
                        return;
                    if (roomData.Model == null || CurrentLoadingRoom.GetGameMap() == null)
                    {
                        Session.SendMessage(
                            new ServerMessage(LibraryParser.OutgoingRequest("OutOfRoomMessageComposer")));
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
                ServerLogManager.LogException("Unable to load room ID [" + Session.GetHabbo().LoadingRoom + "]" + ex);
                ServerLogManager.HandleException(ex, "Yupi.Messages.Handlers.Rooms");
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
                var roomFull = new ServerMessage(LibraryParser.OutgoingRequest("RoomEnterErrorMessageComposer"));
                roomFull.AppendInteger(1);
                return;
            }
            var queuedServerMessage = new QueuedServerMessage(Session.GetConnection());
            var array = CurrentLoadingRoom.GetRoomItemHandler().FloorItems.Values.ToArray();
            var array2 = CurrentLoadingRoom.GetRoomItemHandler().WallItems.Values.ToArray();
            Response.Init(LibraryParser.OutgoingRequest("RoomFloorItemsMessageComposer"));

            if (CurrentLoadingRoom.RoomData.Group != null)
            {
                if (CurrentLoadingRoom.RoomData.Group.AdminOnlyDeco == 1u)
                {
                    Response.AppendInteger(CurrentLoadingRoom.RoomData.Group.Admins.Count + 1);
                    using (var enumerator = CurrentLoadingRoom.RoomData.Group.Admins.Values.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            var current = enumerator.Current;
                            if (Yupi.GetHabboById(current.Id) == null)
                                continue;
                            Response.AppendInteger(current.Id);
                            Response.AppendString(Yupi.GetHabboById(current.Id).UserName);
                        }
                        goto IL_220;
                    }
                }
                Response.AppendInteger(CurrentLoadingRoom.RoomData.Group.Members.Count + 1);
                foreach (var current2 in CurrentLoadingRoom.RoomData.Group.Members.Values)
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
            foreach (var roomItem in array)
            {
                roomItem.Serialize(Response);
            }
            queuedServerMessage.AppendResponse(GetResponse());
            Response.Init(LibraryParser.OutgoingRequest("RoomWallItemsMessageComposer"));
            if (CurrentLoadingRoom.RoomData.Group != null)
            {
                if (CurrentLoadingRoom.RoomData.Group.AdminOnlyDeco == 1u)
                {
                    Response.AppendInteger(CurrentLoadingRoom.RoomData.Group.Admins.Count + 1);
                    using (var enumerator3 = CurrentLoadingRoom.RoomData.Group.Admins.Values.GetEnumerator())
                    {
                        while (enumerator3.MoveNext())
                        {
                            var current3 = enumerator3.Current;
                            Response.AppendInteger(current3.Id);
                            Response.AppendString(Yupi.GetHabboById(current3.Id).UserName);
                        }
                        goto IL_423;
                    }
                }
                Response.AppendInteger(CurrentLoadingRoom.RoomData.Group.Members.Count + 1);
                foreach (var current4 in CurrentLoadingRoom.RoomData.Group.Members.Values)
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
            var array4 = array2;
            foreach (var roomItem2 in array4)
            {
                roomItem2.Serialize(Response);
            }

            queuedServerMessage.AppendResponse(GetResponse());
            Array.Clear(array, 0, array.Length);
            Array.Clear(array2, 0, array2.Length);
            array = null;
            array2 = null;
            CurrentLoadingRoom.GetRoomUserManager().AddUserToRoom(Session, Session.GetHabbo().SpectatorMode);
            Session.GetHabbo().SpectatorMode = false;

            var competition = Yupi.GetGame().GetRoomManager().GetCompetitionManager().Competition;
            if (competition != null)
            {
                if (CurrentLoadingRoom.CheckRights(Session, true))
                {
                    if (!competition.Entries.ContainsKey(CurrentLoadingRoom.RoomData.Id))
                    {
                        competition.AppendEntrySubmitMessage(Response, CurrentLoadingRoom.RoomData.State != 0 ? 4 : 1);
                    }
                    else
                    {
                        if (competition.Entries[CurrentLoadingRoom.RoomData.Id].CompetitionStatus == 3)
                        { }
                        //Competition.AppendEntrySubmitMessage(Response, 0);
                        else if (competition.HasAllRequiredFurnis(CurrentLoadingRoom))
                            competition.AppendEntrySubmitMessage(Response, 2);
                        else
                            competition.AppendEntrySubmitMessage(Response, 3, CurrentLoadingRoom);
                    }
                }
                else if (!CurrentLoadingRoom.CheckRights(Session, true) && competition.Entries.ContainsKey(CurrentLoadingRoom.RoomData.Id))
                {
                    if (Session.GetHabbo().DailyCompetitionVotes > 0)
                        competition.AppendVoteMessage(Response, Session.GetHabbo());
                }
                queuedServerMessage.AppendResponse(GetResponse());
            }
            queuedServerMessage.SendResponse();
            if (Yupi.GetUnixTimeStamp() < Session.GetHabbo().FloodTime && Session.GetHabbo().FloodTime != 0)
            {
                var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("FloodFilterMessageComposer"));
                serverMessage.AppendInteger((Session.GetHabbo().FloodTime - Yupi.GetUnixTimeStamp()));

                Session.SendMessage(serverMessage);
            }
            ClearRoomLoading();

            Poll poll;

            if (!Yupi.GetGame().GetPollManager().TryGetPoll(CurrentLoadingRoom.RoomId, out poll) || Session.GetHabbo().GotPollData(poll.Id))
                return;

            Response.Init(LibraryParser.OutgoingRequest("SuggestPollMessageComposer"));
            poll.Serialize(Response);

            SendResponse();
        }

        internal void WidgetContainers()
        {
            var text = Request.GetString();

            if (Session == null)
                return;

            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("LandingWidgetMessageComposer"));

            if (!string.IsNullOrEmpty(text))
            {
                var array = text.Split(',');

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
            var hotelView = Yupi.GetGame().GetHotelView();

            if (Session?.GetHabbo() == null)
                return;

            if (hotelView.HotelViewPromosIndexers.Count <= 0)
                return;

            var message = hotelView.SmallPromoComposer(new ServerMessage(LibraryParser.OutgoingRequest("LandingPromosMessageComposer")));
            Session.SendMessage(message);
        }

        internal void AcceptPoll()
        {
            var key = Request.GetUInteger();
            var poll = Yupi.GetGame().GetPollManager().Polls[key];

            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PollQuestionsMessageComposer"));

            serverMessage.AppendInteger(poll.Id);
            serverMessage.AppendString(poll.PollName);
            serverMessage.AppendString(poll.Thanks);
            serverMessage.AppendInteger(poll.Questions.Count);

            foreach (var current in poll.Questions)
            {
                var questionNumber = (poll.Questions.IndexOf(current) + 1);

                current.Serialize(serverMessage, questionNumber);
            }

            Response = serverMessage;
            SendResponse();
        }

        internal void RefusePoll()
        {
            var num = Request.GetUInteger();

            Session.GetHabbo().AnsweredPolls.Add(num);

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("INSERT INTO users_polls VALUES (@userid , @pollid , 0 , '0' , '')");
                queryReactor.AddParameter("userid", Session.GetHabbo().Id);
                queryReactor.AddParameter("pollid", num);
                queryReactor.RunQuery();
            }
        }

        internal void AnswerPoll()
        {
            var pollId = Request.GetUInteger();
            var questionId = Request.GetUInteger();
            var num3 = Request.GetInteger();

            var list = new List<string>();

            for (var i = 0; i < num3; i++)
                list.Add(Request.GetString());

            var text = string.Join("\r\n", list);

            var poll = Yupi.GetGame().GetPollManager().TryGetPollById(pollId);

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

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("INSERT INTO users_polls VALUES (@userid , @pollid , @questionid , '1' , @answer)");

                queryReactor.AddParameter("userid", Session.GetHabbo().Id);
                queryReactor.AddParameter("pollid", pollId);
                queryReactor.AddParameter("questionid", questionId);
                queryReactor.AddParameter("answer", text);
                queryReactor.RunQuery();
            }
        }

        public string WallPositionCheck(string wallPosition)
        {
            try
            {
                if (wallPosition.Contains(Convert.ToChar(13)) || wallPosition.Contains(Convert.ToChar(9)))
                    return null;

                var array = wallPosition.Split(' ');

                if (array[2] != "l" && array[2] != "r")
                    return null;

                var array2 = array[0].Substring(3).Split(',');
                var num = int.Parse(array2[0]);
                var num2 = int.Parse(array2[1]);

                if (num >= 0 && num2 >= 0 && num <= 200 && num2 <= 200)
                {
                    var array3 = array[1].Substring(2).Split(',');
                    var num3 = int.Parse(array3[0]);
                    var num4 = int.Parse(array3[1]);

                    return num3 < 0 || num4 < 0 || num3 > 200 || num4 > 200 ? null: string.Concat(":w=", num, ",", num2, " l=", num3, ",", num4, " ", array[2]);
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
            var user = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (user == null)
                return;

            if (user.Statusses.ContainsKey("lay") || user.IsLyingDown || user.RidingHorse || user.IsWalking)
                return;

            if (user.RotBody % 2 != 0)
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

            var currentRoom = Session.GetHabbo().CurrentRoom;
            var text = Request.GetString();
            var text2 = text.Split(' ')[0];
            var msg = text.Substring(text2.Length + 1);
            var colour = Request.GetInteger();

            var roomUserByHabbo = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            var roomUserByHabbo2 = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(text2);

            msg = currentRoom.WordFilter.Aggregate(msg, (current1, current) => Regex.Replace(current1, current, "bobba", RegexOptions.IgnoreCase));

            BlackWord word;

            if (BlackWordsManager.Check(msg, BlackWordType.Hotel, out word))
            {
                var settings = word.TypeSettings;

                if (settings.ShowMessage)
                {
                    Session.SendWhisper("A mensagem enviada tem a palavra: " + word.Word + " Que não é permitida aqui, você poderá ser banido!");
                    return;
                }
            }

            TimeSpan span = DateTime.Now - _floodTime;

            if (span.Seconds > 4)
                _floodCount = 0;

            if (((span.Seconds < 4) && (_floodCount > 5)) && (Session.GetHabbo().Rank < 5))
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

            currentRoom.AddChatlog(Session.GetHabbo().Id, $"<fluister naar {text2}>: {msg}", false);

            Yupi.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.SocialChat);

            var colour2 = colour;

            if (!roomUserByHabbo.IsBot)
                if (colour2 == 2 || (colour2 == 23 && !Session.GetHabbo().HasFuse("fuse_mod")) || colour2 < 0 || colour2 > 29)
                    colour2 = roomUserByHabbo.LastBubble; // or can also be just 0

            roomUserByHabbo.UnIdle();

            var whisp = new ServerMessage(LibraryParser.OutgoingRequest("WhisperMessageComposer"));
            whisp.AppendInteger(roomUserByHabbo.VirtualId);
            whisp.AppendString(msg);
            whisp.AppendInteger(0);
            whisp.AppendInteger(colour2);
            whisp.AppendInteger(0);
            whisp.AppendInteger(-1);

            roomUserByHabbo.GetClient().SendMessage(whisp);

            if (!roomUserByHabbo2.IsBot && roomUserByHabbo2.UserId != roomUserByHabbo.UserId && !roomUserByHabbo2.GetClient().GetHabbo().MutedUsers.Contains(Session.GetHabbo().Id))
                roomUserByHabbo2.GetClient().SendMessage(whisp);

            var roomUserByRank = currentRoom.GetRoomUserManager().GetRoomUserByRank(4);

            if (!roomUserByRank.Any())
                return;

            foreach (var current2 in roomUserByRank)
                if (current2 != null && current2.HabboId != roomUserByHabbo2.HabboId && current2.HabboId != roomUserByHabbo.HabboId && current2.GetClient() != null)
                {
                    var whispStaff = new ServerMessage(LibraryParser.OutgoingRequest("WhisperMessageComposer"));

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
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            var roomUser = room?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUser == null)
                return;

            var message = Request.GetString();
            var bubble = Request.GetInteger();
            var count = Request.GetInteger();

            if (!roomUser.IsBot)
                if (bubble == 2 || (bubble == 23 && !Session.GetHabbo().HasFuse("fuse_mod")) || bubble < 0 || bubble > 29)
                    bubble = roomUser.LastBubble;

            roomUser.Chat(Session, message, false, count, bubble);
        }

        public void Shout()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            var roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            var msg = Request.GetString();
            var bubble = Request.GetInteger();

            if (!roomUserByHabbo.IsBot)
                if (bubble == 2 || (bubble == 23 && !Session.GetHabbo().HasFuse("fuse_mod")) || bubble < 0 || bubble > 29)
                    bubble = roomUserByHabbo.LastBubble; 

            roomUserByHabbo.Chat(Session, msg, true, -1, bubble);
        }

        public void GetFloorPlanUsedCoords()
        {
            Response.Init(LibraryParser.OutgoingRequest("GetFloorPlanUsedCoordsMessageComposer"));

            var room = Session.GetHabbo().CurrentRoom;

            if (room == null)
                Response.AppendInteger(0);
            else
            {
                var coords = room.GetGameMap().CoordinatedItems.Keys.OfType<Point>().ToArray();

                Response.AppendInteger(coords.Count());

                foreach (var point in coords)
                {
                    Response.AppendInteger(point.X);
                    Response.AppendInteger(point.Y);
                }
            }

            SendResponse();
        }

        public void GetFloorPlanDoor()
        {
            var room = Session.GetHabbo().CurrentRoom;

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

            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        public void EnterRoomQueue()
        {
            Session.SendNotif("Currently working on Watch live TV");

            Session.GetHabbo().SpectatorMode = true;

            var forwardToRoom = new ServerMessage(LibraryParser.OutgoingRequest("RoomForwardMessageComposer"));
            forwardToRoom.AppendInteger(1);

            Session.SendMessage(forwardToRoom);
        }

        public void GetCameraRequest()
        {
            try
            {
                int count = Request.GetInteger();
                byte[] bytes = Request.GetBytes(count);
                var outData = Converter.Deflate(bytes);

                var url = WebManager.HttpPostJson(ServerExtraSettings.StoriesApiServerUrl, outData);
                var serializer = new JavaScriptSerializer();

                dynamic jsonArray = serializer.Deserialize<object>(outData);
                string encodedurl = ServerExtraSettings.StoriesApiHost + url;
                encodedurl = encodedurl.Replace("\n", string.Empty);

                int roomId = jsonArray["roomid"];
                long timeStamp = jsonArray["timestamp"];

                using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("INSERT INTO cms_stories_photos_preview (user_id,user_name,room_id,image_preview_url,image_url,type,date,tags) VALUES (@userid,@username,@roomid,@imagepreviewurl,@imageurl,@types,@dates,@tag)");
                    queryReactor.AddParameter("userid", Session.GetHabbo().Id);
                    queryReactor.AddParameter("username", Session.GetHabbo().UserName);
                    queryReactor.AddParameter("roomid", roomId);
                    queryReactor.AddParameter("imagepreviewurl", encodedurl);
                    queryReactor.AddParameter("imageurl", encodedurl);
                    queryReactor.AddParameter("types", "PHOTO");
                    queryReactor.AddParameter("dates", timeStamp);
                    queryReactor.AddParameter("tag", "");
                    queryReactor.RunQuery();
                }

                var message = new ServerMessage(LibraryParser.OutgoingRequest("CameraStorageUrlMessageComposer"));
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

            var code = Request.GetInteger();
            var room = Session.GetHabbo().CurrentRoom;
            var roomData = room?.RoomData;

            if (roomData == null)
                return;

            var competition = Yupi.GetGame().GetRoomManager().GetCompetitionManager().Competition;

            if (competition == null)
                return;

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                if (code == 2)
                {
                    if (competition.Entries.ContainsKey(room.RoomId))
                        return;

                    queryReactor.SetQuery("INSERT INTO rooms_competitions_entries (competition_id, room_id, status) VALUES (@competition_id, @room_id, @status)");

                    queryReactor.AddParameter("competition_id", competition.Id);
                    queryReactor.AddParameter("room_id", room.RoomId);
                    queryReactor.AddParameter("status", 2);
                    queryReactor.RunQuery();
                    competition.Entries.Add(room.RoomId, roomData);

                    var message = new ServerMessage();

                    roomData.CompetitionStatus = 2;
                    competition.AppendEntrySubmitMessage(message, 3, room);

                    Session.SendMessage(message);
                }
                else if (code == 3)
                {
                    if (!competition.Entries.ContainsKey(room.RoomId))
                        return;

                    var entry = competition.Entries[room.RoomId];

                    if (entry == null)
                        return;

                    queryReactor.SetQuery("UPDATE rooms_competitions_entries SET status = @status WHERE competition_id = @competition_id AND room_id = @roomid");

                    queryReactor.AddParameter("status", 3);
                    queryReactor.AddParameter("competition_id", competition.Id);
                    queryReactor.AddParameter("roomid", room.RoomId);
                    queryReactor.RunQuery();
                    roomData.CompetitionStatus = 3;

                    var message = new ServerMessage();
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

            var room = Session.GetHabbo().CurrentRoom;

            var roomData = room?.RoomData;

            if (roomData == null)
                return;

            var competition = Yupi.GetGame().GetRoomManager().GetCompetitionManager().Competition;

            if (competition == null)
                return;

            if (!competition.Entries.ContainsKey(room.RoomId))
                return;

            var entry = competition.Entries[room.RoomId];

            entry.CompetitionVotes++;
            Session.GetHabbo().DailyCompetitionVotes--;

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE rooms_competitions_entries SET votes = @votes WHERE competition_id = @competition_id AND room_id = @roomid");

                queryReactor.AddParameter("votes", entry.CompetitionVotes);
                queryReactor.AddParameter("competition_id", competition.Id);
                queryReactor.AddParameter("roomid", room.RoomId);
                queryReactor.RunQuery();
                queryReactor.RunFastQuery("UPDATE users_stats SET daily_competition_votes = " + Session.GetHabbo().DailyCompetitionVotes + " WHERE id = " + Session.GetHabbo().Id);
            }

            var message = new ServerMessage();
            competition.AppendVoteMessage(message, Session.GetHabbo());

            Session.SendMessage(message);
        }
    }
}