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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Achievements.Structs;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Game.Users.Badges.Models;
using Yupi.Emulator.Game.Users.Messenger.Structs;
using Yupi.Emulator.Game.Users.Relationships;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class GameClientMessageHandler.
    /// </summary>
    internal partial class GameClientMessageHandler
    {
        /// <summary>
        ///     Sends the bully report.
        /// </summary>
        internal void  SendBullyReport()
        {
            uint reportedId = Request.GetUInteger();

            Yupi.GetGame()
                .GetModerationTool()
                .SendNewTicket(Session, 104, 9, reportedId, string.Empty, new List<string>());

            Response.Init(PacketLibraryManager.SendRequest("BullyReportSentMessageComposer"));
            Response.AppendInteger(0);
            SendResponse();
        }

        /// <summary>
        ///     Opens the bully reporting.
        /// </summary>
        internal void  OpenBullyReporting()
        {
            Response.Init(PacketLibraryManager.SendRequest("OpenBullyReportMessageComposer"));
            Response.AppendInteger(0);
            SendResponse();
        }

        /// <summary>
        ///     Opens the quests.
        /// </summary>
        internal void  OpenQuests()
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("QuestListMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendBool(Request != null);
            Session.SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Retrieves the citizenship.
        /// </summary>
        internal void RetriveCitizenShipStatus()
        {
            GetResponse().Init(PacketLibraryManager.SendRequest("CitizenshipStatusMessageComposer"));
            GetResponse().AppendString(Request.GetString());
            GetResponse().AppendInteger(4);
            GetResponse().AppendInteger(4);
        }

        /// <summary>
        ///     Loads the club gifts.
        /// </summary>
        internal void LoadClubGifts()
        {
            if (Session?.GetHabbo() == null)
                return;

            Session.GetHabbo().GetSubscriptionManager().GetSubscription();

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();
            simpleServerMessageBuffer.Init(PacketLibraryManager.SendRequest("LoadCatalogClubGiftsMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(0); // i
            simpleServerMessageBuffer.AppendInteger(0); // i2
            simpleServerMessageBuffer.AppendInteger(1);
        }

        /// <summary>
        ///     Gets the user tags.
        /// </summary>
        internal void GetUserTags()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Request.GetUInteger());

            if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
                return;

            Response.Init(PacketLibraryManager.SendRequest("UserTagsMessageComposer"));
            Response.AppendInteger(roomUserByHabbo.GetClient().GetHabbo().Id);
            Response.AppendInteger(roomUserByHabbo.GetClient().GetHabbo().Tags.Count);

            foreach (string current in roomUserByHabbo.GetClient().GetHabbo().Tags)
                Response.AppendString(current);

            SendResponse();

            if (Session != roomUserByHabbo.GetClient())
                return;

            if (Session.GetHabbo().Tags.Count >= 5)
                Yupi.GetGame()
                    .GetAchievementManager()
                    .ProgressUserAchievement(roomUserByHabbo.GetClient(), "ACH_UserTags", 5);
        }

        /// <summary>
        ///     Gets the user badges.
        /// </summary>
        internal void GetUserBadges()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Request.GetUInteger());

            if (roomUserByHabbo != null && !roomUserByHabbo.IsBot && roomUserByHabbo.GetClient() != null &&
                roomUserByHabbo.GetClient().GetHabbo() != null)
            {
                Session.GetHabbo().LastSelectedUser = roomUserByHabbo.UserId;
                Response.Init(PacketLibraryManager.SendRequest("UserBadgesMessageComposer"));
                Response.AppendInteger(roomUserByHabbo.GetClient().GetHabbo().Id);

                Response.StartArray();
                foreach (
                    Badge badge in
                        roomUserByHabbo.GetClient()
                            .GetHabbo()
                            .GetBadgeComponent()
                            .BadgeList.Values.Cast<Badge>()
                            .Where(badge => badge.Slot > 0).Take(5))
                {
                    Response.AppendInteger(badge.Slot);
                    Response.AppendString(badge.Code);

                    Response.SaveArray();
                }

                Response.EndArray();
                SendResponse();
            }
        }

        /// <summary>
        ///     Gives the respect.
        /// </summary>
        internal void GiveRespect()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || Session.GetHabbo().DailyRespectPoints <= 0)
                return;

            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Request.GetUInteger());

            if (roomUserByHabbo == null || roomUserByHabbo.GetClient().GetHabbo().Id == Session.GetHabbo().Id ||
                roomUserByHabbo.IsBot)
                return;

            Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_RespectGiven", 1, true);
            Yupi.GetGame()
                .GetAchievementManager()
                .ProgressUserAchievement(roomUserByHabbo.GetClient(), "ACH_RespectEarned", 1, true);

            Session.GetHabbo().DailyRespectPoints--;
            roomUserByHabbo.GetClient().GetHabbo().Respect++;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery("UPDATE users_stats SET respect = respect + 1 WHERE id = " +
                                          roomUserByHabbo.GetClient().GetHabbo().Id +
                                          " LIMIT 1;UPDATE users_stats SET daily_respect_points = daily_respect_points - 1 WHERE id= " +
                                          Session.GetHabbo().Id + " LIMIT 1");

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("GiveRespectsMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(roomUserByHabbo.GetClient().GetHabbo().Id);
            simpleServerMessageBuffer.AppendInteger(roomUserByHabbo.GetClient().GetHabbo().Respect);
            room.SendMessage(simpleServerMessageBuffer);

            SimpleServerMessageBuffer thumbsUp = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("RoomUserActionMessageComposer"));
            thumbsUp.AppendInteger(room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().UserName).VirtualId);
            thumbsUp.AppendInteger(7);
            room.SendMessage(thumbsUp);
        }

        /// <summary>
        ///     Applies the effect.
        /// </summary>
        internal void ApplyEffect()
        {
            int effectId = Request.GetInteger();
            RoomUser roomUserByHabbo =
                Yupi.GetGame()
                    .GetRoomManager()
                    .GetRoom(Session.GetHabbo().CurrentRoomId)
                    .GetRoomUserManager()
                    .GetRoomUserByHabbo(Session.GetHabbo().UserName);
            if (!roomUserByHabbo.RidingHorse)
                Session.GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(effectId);
        }

        internal void EnableInventoryEffect()
        {
            EnableEffect();
        }

        /// <summary>
        ///     Enables the effect.
        /// </summary>
        internal void EnableEffect()
        {
            Room currentRoom = Session.GetHabbo().CurrentRoom;
            RoomUser roomUserByHabbo = currentRoom?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            int num = Request.GetInteger();
            if (roomUserByHabbo.RidingHorse)
                return;
            if (num == 0)
            {
                Session.GetHabbo()
                    .GetAvatarEffectsInventoryComponent()
                    .StopEffect(Session.GetHabbo().GetAvatarEffectsInventoryComponent().CurrentEffect);
                return;
            }
            Session.GetHabbo().GetAvatarEffectsInventoryComponent().ActivateEffect(num);
        }

        /// <summary>
        ///     Mutes the user.
        /// </summary>
        internal void MuteUser()
        {
            uint num = Request.GetUInteger();

            Request.GetUInteger();

            uint num2 = Request.GetUInteger();

            Room currentRoom = Session.GetHabbo().CurrentRoom;

            if ((currentRoom == null || (currentRoom.RoomData.WhoCanBan == 0 && !currentRoom.CheckRights(Session, true)) ||
                 (currentRoom.RoomData.WhoCanBan == 1 && !currentRoom.CheckRights(Session))) &&
                Session.GetHabbo().Rank < Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]))
                return;

            RoomUser roomUserByHabbo = currentRoom?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(num).UserName);

            if (roomUserByHabbo == null)
                return;

            if (roomUserByHabbo.GetClient().GetHabbo().Rank >= Session.GetHabbo().Rank)
                return;

            if (currentRoom.MutedUsers.ContainsKey(num))
            {
                if (currentRoom.MutedUsers[num] >= (ulong) Yupi.GetUnixTimeStamp())
                    return;
                currentRoom.MutedUsers.Remove(num);
            }

            currentRoom.MutedUsers.Add(num, uint.Parse((Yupi.GetUnixTimeStamp() + checked(num2*60u)).ToString()));

            roomUserByHabbo.GetClient()
                .SendNotif(string.Format(Yupi.GetLanguage().GetVar("room_owner_has_mute_user"), num2));
        }

        /// <summary>
        ///     Gets the user information.
        /// </summary>
        internal void GetUserInfo()
        {
            GetResponse().Init(PacketLibraryManager.SendRequest("UpdateUserDataMessageComposer"));
            GetResponse().AppendInteger(-1);
            GetResponse().AppendString(Session.GetHabbo().Look);
            GetResponse().AppendString(Session.GetHabbo().Gender.ToLower());
            GetResponse().AppendString(Session.GetHabbo().Motto);
            GetResponse().AppendInteger(Session.GetHabbo().AchievementPoints);
            SendResponse();
            GetResponse().Init(PacketLibraryManager.SendRequest("AchievementPointsMessageComposer"));
            GetResponse().AppendInteger(Session.GetHabbo().AchievementPoints);
            SendResponse();
        }

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        internal void GetBalance()
        {
            if (Session?.GetHabbo() == null)
                return;

            Session.GetHabbo().UpdateCreditsBalance();
            Session.GetHabbo().UpdateSeasonalCurrencyBalance();
        }

        /// <summary>
        ///     Gets the subscription data.
        /// </summary>
        internal void GetSubscriptionData()
        {
            Session.GetHabbo().SerializeClub();
        }

        /// <summary>
        ///     Loads the settings.
        /// </summary>
        internal void GetVoume()
        {
            UserPreferences preferences = Session.GetHabbo().Preferences;
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("LoadVolumeMessageComposer"));

            simpleServerMessageBuffer.AppendIntegersArray(preferences.Volume, ',', 3, 0, 100);

            simpleServerMessageBuffer.AppendBool(preferences.PreferOldChat);
            simpleServerMessageBuffer.AppendBool(preferences.IgnoreRoomInvite);
            simpleServerMessageBuffer.AppendBool(preferences.DisableCameraFollow);
            simpleServerMessageBuffer.AppendInteger(3); // collapse friends (3 = no)
            simpleServerMessageBuffer.AppendInteger(preferences.ChatColor); //bubble
            Session.SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Saves the settings.
        /// </summary>
        internal void SaveVolume()
        {
            int num = Request.GetInteger();
            int num2 = Request.GetInteger();
            int num3 = Request.GetInteger();
            Session.GetHabbo().Preferences.Volume = num + "," + num2 + "," + num3;
            Session.GetHabbo().Preferences.Save();
        }

        /// <summary>
        ///     Sets the chat preferrence.
        /// </summary>
        internal void SetChatPreferrence()
        {
            bool enable = Request.GetBool();
            Session.GetHabbo().Preferences.PreferOldChat = enable;
            Session.GetHabbo().Preferences.Save();
        }

        internal void SetInvitationsPreference()
        {
            bool enable = Request.GetBool();
            Session.GetHabbo().Preferences.IgnoreRoomInvite = enable;
            Session.GetHabbo().Preferences.Save();
        }

        internal void SetRoomCameraPreferences()
        {
            bool enable = Request.GetBool();
            Session.GetHabbo().Preferences.DisableCameraFollow = enable;
            Session.GetHabbo().Preferences.Save();
        }

        /// <summary>
        ///     Gets the badges.
        /// </summary>
        internal void GetBadges()
        {
            Session.SendMessage(Session.GetHabbo().GetBadgeComponent().Serialize());
        }

        /// <summary>
        ///     Updates the badges.
        /// </summary>
        internal void UpdateBadges()
        {
            Session.GetHabbo().GetBadgeComponent().ResetSlots();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"UPDATE users_badges SET badge_slot = 0 WHERE user_id = {Session.GetHabbo().Id}");

            for (int i = 0; i < 5; i++)
            {
                int slot = Request.GetInteger();
                string code = Request.GetString();

                if (code.Length == 0)
                    continue;

                if (!Session.GetHabbo().GetBadgeComponent().HasBadge(code) || slot < 1 || slot > 5)
                    return;

                Session.GetHabbo().GetBadgeComponent().GetBadge(code).Slot = slot;

                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryreactor2.SetQuery("UPDATE users_badges SET badge_slot = " + slot +
                                           " WHERE badge_id = @badge AND user_id = " + Session.GetHabbo().Id);
                    queryreactor2.AddParameter("badge", code);
                    queryreactor2.RunQuery();
                }
            }

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("UserBadgesMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(Session.GetHabbo().Id);

            simpleServerMessageBuffer.StartArray();

            foreach (
                Badge badge in
                    Session.GetHabbo().GetBadgeComponent().BadgeList.Values.Cast<Badge>().Where(badge => badge.Slot > 0)
                )
            {
                simpleServerMessageBuffer.AppendInteger(badge.Slot);
                simpleServerMessageBuffer.AppendString(badge.Code);
                simpleServerMessageBuffer.SaveArray();
            }

            simpleServerMessageBuffer.EndArray();

            if (Session.GetHabbo().InRoom &&
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId) != null)
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId).SendMessage(simpleServerMessageBuffer);
            else
                Session.SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Gets the achievements.
        /// </summary>
        internal void GetAchievements() => Yupi.GetGame().GetAchievementManager().GetList(Session);

        /// <summary>
        ///     Prepares the campaing.
        /// </summary>
        internal void PrepareCampaing()
        {
            string text = Request.GetString();
            Response.Init(PacketLibraryManager.SendRequest("SendCampaignBadgeMessageComposer"));
            Response.AppendString(text);
            Response.AppendBool(Session.GetHabbo().GetBadgeComponent().HasBadge(text));
            SendResponse();
        }

        /// <summary>
        ///     Gets the friends count.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Int32.</returns>
        internal static int GetFriendsCount(uint userId)
        {
            int result;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    "SELECT COUNT(*) FROM messenger_friendships WHERE user_one_id = @id OR user_two_id = @id;");
                queryReactor.AddParameter("id", userId);

                result = queryReactor.GetInteger();
            }

            return result;
        }

        /// <summary>
        ///     Loads the profile.
        /// </summary>
        internal void LoadProfile()
        {
            uint userId = Request.GetUInteger();
            Request.GetBool();

            Habbo habbo = Yupi.GetHabboById(userId);
            if (habbo == null)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("user_not_found"));
                return;
            }
            DateTime createTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(habbo.CreateDate);

            Response.Init(PacketLibraryManager.SendRequest("UserProfileMessageComposer"));
            Response.AppendInteger(habbo.Id);
            Response.AppendString(habbo.UserName);
            Response.AppendString(habbo.Look);
            Response.AppendString(habbo.Motto);
            Response.AppendString(createTime.ToString("dd/MM/yyyy"));
            Response.AppendInteger(habbo.AchievementPoints);
            Response.AppendInteger(GetFriendsCount(userId));
            Response.AppendBool(habbo.Id != Session.GetHabbo().Id &&
                                Session.GetHabbo().GetMessenger().FriendshipExists(habbo.Id));
            Response.AppendBool(habbo.Id != Session.GetHabbo().Id &&
                                !Session.GetHabbo().GetMessenger().FriendshipExists(habbo.Id) &&
                                Session.GetHabbo().GetMessenger().RequestExists(habbo.Id));
            Response.AppendBool(Yupi.GetGame().GetClientManager().GetClientByUserId(habbo.Id) != null);
            HashSet<GroupMember> groups = Yupi.GetGame().GetGroupManager().GetUserGroups(habbo.Id);
            Response.AppendInteger(groups.Count);
            foreach (Group @group in groups.Select(groupUs => Yupi.GetGame().GetGroupManager().GetGroup(groupUs.GroupId))
                )
                if (@group != null)
                {
                    Response.AppendInteger(@group.Id);
                    Response.AppendString(@group.Name);
                    Response.AppendString(@group.Badge);
                    Response.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(@group.Colour1, true));
                    Response.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(@group.Colour2, false));
                    Response.AppendBool(@group.Id == habbo.FavouriteGroup);
                    Response.AppendInteger(-1);
                    Response.AppendBool(@group.Forum.Id != 0);
                }
                else
                {
                    Response.AppendInteger(1);
                    Response.AppendString("THIS GROUP IS INVALID");
                    Response.AppendString("");
                    Response.AppendString("");
                    Response.AppendString("");
                    Response.AppendBool(false);
                    Response.AppendInteger(-1);
                    Response.AppendBool(false);
                }
            if (habbo.PreviousOnline == 0)
                Response.AppendInteger(-1);
            else if (Yupi.GetGame().GetClientManager().GetClientByUserId(habbo.Id) == null)
                Response.AppendInteger(Yupi.GetUnixTimeStamp() - habbo.PreviousOnline);
            else
                Response.AppendInteger(Yupi.GetUnixTimeStamp() - habbo.LastOnline);

            Response.AppendBool(true);
            SendResponse();
            Response.Init(PacketLibraryManager.SendRequest("UserBadgesMessageComposer"));
            Response.AppendInteger(habbo.Id);
            Response.StartArray();

            foreach (
                Badge badge in habbo.GetBadgeComponent().BadgeList.Values.Cast<Badge>().Where(badge => badge.Slot > 0))
            {
                Response.AppendInteger(badge.Slot);
                Response.AppendString(badge.Code);

                Response.SaveArray();
            }

            Response.EndArray();
            SendResponse();
        }

        /// <summary>
        ///     Changes the look.
        /// </summary>
        internal void ChangeLook()
        {
            string text = Request.GetString().ToUpper();
            string text2 = Request.GetString();

            text2 = Yupi.FilterFigure(text2);

            Session.GetHabbo().Look = text2;
            Session.GetHabbo().Gender = text.ToLower() == "f" ? "f" : "m";

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"UPDATE users SET look = @look, gender = @gender WHERE id = {Session.GetHabbo().Id}");
                queryReactor.AddParameter("look", text2);
                queryReactor.AddParameter("gender", text);
                queryReactor.RunQuery();
            }

            Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_AvatarLooks", 1);

            Session.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.SendRequest("UpdateAvatarAspectMessageComposer"));
            Session.GetMessageHandler().GetResponse().AppendString(Session.GetHabbo().Look);
            Session.GetMessageHandler().GetResponse().AppendString(Session.GetHabbo().Gender.ToUpper());
            Session.GetMessageHandler().SendResponse();

            Session.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.SendRequest("UpdateUserDataMessageComposer"));
            Session.GetMessageHandler().GetResponse().AppendInteger(-1);
            Session.GetMessageHandler().GetResponse().AppendString(Session.GetHabbo().Look);
            Session.GetMessageHandler().GetResponse().AppendString(Session.GetHabbo().Gender.ToLower());
            Session.GetMessageHandler().GetResponse().AppendString(Session.GetHabbo().Motto);
            Session.GetMessageHandler().GetResponse().AppendInteger(Session.GetHabbo().AchievementPoints);
            Session.GetMessageHandler().SendResponse();

            if (!Session.GetHabbo().InRoom)
                return;

            Room currentRoom = Session.GetHabbo().CurrentRoom;

            RoomUser roomUserByHabbo = currentRoom?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("UpdateUserDataMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(roomUserByHabbo.VirtualId); //SimpleServerMessageBuffer.AppendInt32(-1);
            simpleServerMessageBuffer.AppendString(Session.GetHabbo().Look);
            simpleServerMessageBuffer.AppendString(Session.GetHabbo().Gender.ToLower());
            simpleServerMessageBuffer.AppendString(Session.GetHabbo().Motto);
            simpleServerMessageBuffer.AppendInteger(Session.GetHabbo().AchievementPoints);
            currentRoom.SendMessage(simpleServerMessageBuffer);

            if (Session.GetHabbo().GetMessenger() != null)
                Session.GetHabbo().GetMessenger().OnStatusChanged(true);
        }

        /// <summary>
        ///     Changes the motto.
        /// </summary>
        internal void ChangeMotto()
        {
            string text = Request.GetString();

            if (text == Session.GetHabbo().Motto)
                return;

            Session.GetHabbo().Motto = text;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery($"UPDATE users SET motto = @motto WHERE id = '{Session.GetHabbo().Id}'");
                queryReactor.AddParameter("motto", text);
                queryReactor.RunQuery();
            }

            if (Session.GetHabbo().InRoom)
            {
                Room currentRoom = Session.GetHabbo().CurrentRoom;
                RoomUser roomUserByHabbo = currentRoom?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

                if (roomUserByHabbo == null)
                    return;

                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("UpdateUserDataMessageComposer"));
                simpleServerMessageBuffer.AppendInteger(roomUserByHabbo.VirtualId); //SimpleServerMessageBuffer.AppendInt32(-1);
                simpleServerMessageBuffer.AppendString(Session.GetHabbo().Look);
                simpleServerMessageBuffer.AppendString(Session.GetHabbo().Gender.ToLower());
                simpleServerMessageBuffer.AppendString(Session.GetHabbo().Motto);
                simpleServerMessageBuffer.AppendInteger(Session.GetHabbo().AchievementPoints);
                currentRoom.SendMessage(simpleServerMessageBuffer);
            }

            Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_Motto", 1);
        }

        /// <summary>
        ///     Gets the wardrobe.
        /// </summary>
        internal void GetWardrobe()
        {
            GetResponse().Init(PacketLibraryManager.SendRequest("LoadWardrobeMessageComposer"));
            GetResponse().AppendInteger(0);
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"SELECT slot_id, look, gender FROM users_wardrobe WHERE user_id = {Session.GetHabbo().Id}");
                DataTable table = queryReactor.GetTable();
                if (table == null)
                    GetResponse().AppendInteger(0);
                else
                {
                    GetResponse().AppendInteger(table.Rows.Count);
                    foreach (DataRow dataRow in table.Rows)
                    {
                        GetResponse().AppendInteger(Convert.ToUInt32(dataRow["slot_id"]));
                        GetResponse().AppendString((string) dataRow["look"]);
                        GetResponse().AppendString(dataRow["gender"].ToString().ToUpper());
                    }
                }
                SendResponse();
            }
        }

        /// <summary>
        ///     Saves the wardrobe.
        /// </summary>
        internal void SaveWardrobe()
        {
            uint num = Request.GetUInteger();
            string text = Request.GetString();
            string text2 = Request.GetString().ToUpper() == "F" ? "F" : "M";

            text = Yupi.FilterFigure(text);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(string.Concat("SELECT null FROM users_wardrobe WHERE user_id = ",
                    Session.GetHabbo().Id, " AND slot_id = ", num));
                queryReactor.AddParameter("look", text);
                queryReactor.AddParameter("gender", text2);
                if (queryReactor.GetRow() != null)
                {
                    queryReactor.SetQuery(
                        string.Concat("UPDATE users_wardrobe SET look = @look, gender = @gender WHERE user_id = ",
                            Session.GetHabbo().Id, " AND slot_id = ", num, ";"));
                    queryReactor.AddParameter("look", text);
                    queryReactor.AddParameter("gender", text2);
                    queryReactor.RunQuery();
                }
                else
                {
                    queryReactor.SetQuery(
                        string.Concat("INSERT INTO users_wardrobe (user_id,slot_id,look,gender) VALUES (",
                            Session.GetHabbo().Id, ",", num, ",@look,@gender)"));
                    queryReactor.AddParameter("look", text);
                    queryReactor.AddParameter("gender", text2);
                    queryReactor.RunQuery();
                }
            }
        }

        /// <summary>
        ///     Gets the pets inventory.
        /// </summary>
        internal void GetPetsInventory()
        {
            if (Session.GetHabbo().GetInventoryComponent() == null)
                return;

            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializePetInventory());
        }

        /// <summary>
        ///     Gets the bots inventory.
        /// </summary>
        internal void GetBotInv()
        {
            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializeBotInventory());
            SendResponse();
        }

        /// <summary>
        ///     Checks the name.
        /// </summary>
        internal void CheckName()
        {
            string text = Request.GetString();
            if (text.ToLower() == Session.GetHabbo().UserName.ToLower())
            {
                Response.Init(PacketLibraryManager.SendRequest("NameChangedUpdatesMessageComposer"));
                Response.AppendInteger(0);
                Response.AppendString(text);
                Response.AppendInteger(0);
                SendResponse();
                return;
            }
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT username FROM users WHERE Username=@name LIMIT 1");
                queryReactor.AddParameter("name", text);
                string @string = queryReactor.GetString();
                char[] array = text.ToLower().ToCharArray();
                const string source = "abcdefghijklmnopqrstuvwxyz1234567890.,_-;:?!@áéíóúÁÉÍÓÚñÑÜüÝý ";
                char[] array2 = array;
                if (array2.Any(c => !source.Contains(char.ToLower(c))))
                {
                    Response.Init(PacketLibraryManager.SendRequest("NameChangedUpdatesMessageComposer"));
                    Response.AppendInteger(4);
                    Response.AppendString(text);
                    Response.AppendInteger(0);
                    SendResponse();
                    return;
                }
                if (text.ToLower().Contains("mod") || text.ToLower().Contains("m0d") || text.Contains(" ") ||
                    text.ToLower().Contains("admin"))
                {
                    Response.Init(PacketLibraryManager.SendRequest("NameChangedUpdatesMessageComposer"));
                    Response.AppendInteger(4);
                    Response.AppendString(text);
                    Response.AppendInteger(0);
                    SendResponse();
                }
                else if (text.Length > 15)
                {
                    Response.Init(PacketLibraryManager.SendRequest("NameChangedUpdatesMessageComposer"));
                    Response.AppendInteger(3);
                    Response.AppendString(text);
                    Response.AppendInteger(0);
                    SendResponse();
                }
                else if (text.Length < 3)
                {
                    Response.Init(PacketLibraryManager.SendRequest("NameChangedUpdatesMessageComposer"));
                    Response.AppendInteger(2);
                    Response.AppendString(text);
                    Response.AppendInteger(0);
                    SendResponse();
                }
                else if (string.IsNullOrWhiteSpace(@string))
                {
                    Response.Init(PacketLibraryManager.SendRequest("NameChangedUpdatesMessageComposer"));
                    Response.AppendInteger(0);
                    Response.AppendString(text);
                    Response.AppendInteger(0);
                    SendResponse();
                }
                else
                {
                    queryReactor.SetQuery("SELECT tag FROM users_tags ORDER BY RAND() LIMIT 3");
                    DataTable table = queryReactor.GetTable();
                    Response.Init(PacketLibraryManager.SendRequest("NameChangedUpdatesMessageComposer"));
                    Response.AppendInteger(5);
                    Response.AppendString(text);
                    Response.AppendInteger(table.Rows.Count);
                    foreach (DataRow dataRow in table.Rows)
                        Response.AppendString($"{text}{dataRow[0]}");
                    SendResponse();
                }
            }
        }

        /// <summary>
        ///     Changes the name.
        /// </summary>
        internal void ChangeName()
        {
            string text = Request.GetString();
            string userName = Session.GetHabbo().UserName;

            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("SELECT username FROM users WHERE Username=@name LIMIT 1");
                    queryReactor.AddParameter("name", text);
                    string @String = queryReactor.GetString();

                    if (!string.IsNullOrWhiteSpace(String) &&
                        !string.Equals(userName, text, StringComparison.CurrentCultureIgnoreCase))
                        return;

                    queryReactor.SetQuery(
                        "UPDATE users SET Username = @newname, last_name_change = @timestamp WHERE id = @userid");
                    queryReactor.AddParameter("newname", text);
                    queryReactor.AddParameter("timestamp", Yupi.GetUnixTimeStamp() + 43200);
                    queryReactor.AddParameter("userid", Session.GetHabbo().UserName);
                    queryReactor.RunQuery();

                    Session.GetHabbo().LastChange = Yupi.GetUnixTimeStamp() + 43200;
                    Session.GetHabbo().UserName = text;
                    Response.Init(PacketLibraryManager.SendRequest("UpdateUsernameMessageComposer"));
                    Response.AppendInteger(0);
                    Response.AppendString(text);
                    Response.AppendInteger(0);
                    SendResponse();
                    Response.Init(PacketLibraryManager.SendRequest("UpdateUserDataMessageComposer"));
                    Response.AppendInteger(-1);
                    Response.AppendString(Session.GetHabbo().Look);
                    Response.AppendString(Session.GetHabbo().Gender.ToLower());
                    Response.AppendString(Session.GetHabbo().Motto);
                    Response.AppendInteger(Session.GetHabbo().AchievementPoints);
                    SendResponse();
                    Session.GetHabbo().CurrentRoom.GetRoomUserManager().UpdateUser(userName, text);
                    if (Session.GetHabbo().CurrentRoom != null)
                    {
                        Response.Init(PacketLibraryManager.SendRequest("UserUpdateNameInRoomMessageComposer"));
                        Response.AppendInteger(Session.GetHabbo().Id);
                        Response.AppendInteger(Session.GetHabbo().CurrentRoom.RoomId);
                        Response.AppendString(text);
                    }
                    foreach (RoomData current in Session.GetHabbo().UsersRooms)
                    {
                        current.Owner = text;
                        current.SerializeRoomData(Response, Session, false, true);
                        Room room = Yupi.GetGame().GetRoomManager().GetRoom(current.Id);
                        if (room != null)
                            room.RoomData.Owner = text;
                    }
                    foreach (MessengerBuddy current2 in Session.GetHabbo().GetMessenger().Friends.Values)
                        if (current2.Client != null)
                            foreach (
                                MessengerBuddy current3 in
                                    current2.Client.GetHabbo()
                                        .GetMessenger()
                                        .Friends.Values.Where(current3 => current3.UserName == userName))
                            {
                                current3.UserName = text;
                                current3.Serialize(Response, Session);
                            }
                }
            }
        }

        /// <summary>
        ///     Gets the relationships.
        /// </summary>
        internal void GetRelationships()
        {
            uint userId = Request.GetUInteger();
            Habbo habboForId = Yupi.GetHabboById(userId);
            if (habboForId == null)
                return;
            Random rand = new Random();
            habboForId.Relationships = (
                from x in habboForId.Relationships
                orderby rand.Next()
                select x).ToDictionary(item => item.Key,
                    item => item.Value);
            int num = habboForId.Relationships.Count(x => x.Value.Type == 1);
            int num2 = habboForId.Relationships.Count(x => x.Value.Type == 2);
            int num3 = habboForId.Relationships.Count(x => x.Value.Type == 3);
            Response.Init(PacketLibraryManager.SendRequest("RelationshipMessageComposer"));
            Response.AppendInteger(habboForId.Id);
            Response.AppendInteger(habboForId.Relationships.Count);
            foreach (Relationship current in habboForId.Relationships.Values)
            {
                Habbo habboForId2 = Yupi.GetHabboById(Convert.ToUInt32(current.UserId));
                if (habboForId2 == null)
                {
                    Response.AppendInteger(0);
                    Response.AppendInteger(0);
                    Response.AppendInteger(0);
                    Response.AppendString("Placeholder");
                    Response.AppendString("hr-115-42.hd-190-1.ch-215-62.lg-285-91.sh-290-62");
                }
                else
                {
                    Response.AppendInteger(current.Type);
                    Response.AppendInteger(current.Type == 1 ? num : (current.Type == 2 ? num2 : num3));
                    Response.AppendInteger(current.UserId);
                    Response.AppendString(habboForId2.UserName);
                    Response.AppendString(habboForId2.Look);
                }
            }
            SendResponse();
        }

        /// <summary>
        ///     Sets the relationship.
        /// </summary>
        internal void SetRelationship()
        {
            uint userId = Request.GetUInteger();
            uint targetId = Request.GetUInteger();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                if (targetId == 0)
                {
                    queryReactor.SetQuery("SELECT id FROM users_relationships WHERE user_id=@id AND target=@target LIMIT 1");
                    queryReactor.AddParameter("id", Session.GetHabbo().Id);
                    queryReactor.AddParameter("target", userId);

                    object integerResult = queryReactor.GetInteger();

                    int integer;

                    int.TryParse(integerResult.ToString(), out integer);

                    if (integer > 0 && Session.GetHabbo().Relationships.ContainsKey(integer))
                    {
                        queryReactor.SetQuery("DELETE FROM users_relationships WHERE user_id=@id AND target=@target LIMIT 1");
                        queryReactor.AddParameter("id", Session.GetHabbo().Id);
                        queryReactor.AddParameter("target", userId);
                        queryReactor.RunQuery();

                        Session.GetHabbo().Relationships.Remove(integer);
                    }                 
                }
                else
                {
                    queryReactor.SetQuery("SELECT id FROM users_relationships WHERE user_id=@id AND target=@target LIMIT 1");
                    queryReactor.AddParameter("id", Session.GetHabbo().Id);
                    queryReactor.AddParameter("target", userId);

                    object integerResult = queryReactor.GetInteger();

                    int integer;

                    int.TryParse(integerResult.ToString(), out integer);

                    if (integer > 0 && Session.GetHabbo().Relationships.ContainsKey(integer))
                    {
                        queryReactor.SetQuery("DELETE FROM users_relationships WHERE user_id=@id AND target=@target LIMIT 1");
                        queryReactor.AddParameter("id", Session.GetHabbo().Id);
                        queryReactor.AddParameter("target", userId);
                        queryReactor.RunQuery();

                        Session.GetHabbo().Relationships.Remove(integer);
                    }
                }

                if (userId > 0 && targetId > 0)
                {
                    queryReactor.SetQuery("INSERT INTO users_relationships (user_id, target, type) VALUES (@id, @target, @type)");
                    queryReactor.AddParameter("id", Session.GetHabbo().Id);
                    queryReactor.AddParameter("target", userId);
                    queryReactor.AddParameter("type", targetId);

                    object integerResult = queryReactor.InsertQuery();

                    int relationShipId;

                    int.TryParse(integerResult.ToString(), out relationShipId);

                    if(relationShipId > 0)
                        Session.GetHabbo().Relationships.Add(relationShipId, new Relationship(relationShipId, (int)userId, (int)targetId));
                }
            }

            if (userId > 0)
            {
                GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

                if (clientByUserId != null)
                    Session.GetHabbo().GetMessenger().UpdateFriend(userId, clientByUserId, true);
            }
        }

        /// <summary>
        ///     Talentses this instance.
        /// </summary>
        /// <exception cref="System.NullReferenceException"></exception>
        internal void Talents()
        {
            string trackType = Request.GetString();

            List<Talent> talents = Yupi.GetGame().GetTalentManager().GetTalents(trackType, -1);

            int failLevel = -1;

            if (talents == null)
                return;

            Response.Init(PacketLibraryManager.SendRequest("TalentsTrackMessageComposer"));

            Response.AppendString(trackType);
            Response.AppendInteger(talents.Count);

            foreach (Talent current in talents)
            {
                Response.AppendInteger(current.Level);

                int nm = failLevel == -1 ? 1 : 0;
                Response.AppendInteger(nm);

                List<Talent> talents2 = Yupi.GetGame().GetTalentManager().GetTalents(trackType, current.Id);

                Response.AppendInteger(talents2.Count);

                foreach (Talent current2 in talents2)
                {
                    if (current2.GetAchievement() == null)
                        throw new NullReferenceException(
                            $"The following talent achievement can't be found: {current2.AchievementGroup}");

                    int num = failLevel != -1 && failLevel < current2.Level
                        ? 0
                        : Session.GetHabbo().GetAchievementData(current2.AchievementGroup) == null
                            ? 1
                            : Session.GetHabbo().GetAchievementData(current2.AchievementGroup).Level >=
                              current2.AchievementLevel
                                ? 2
                                : 1;

                    Response.AppendInteger(current2.GetAchievement().Id);
                    Response.AppendInteger(0);
                    Response.AppendString($"{current2.AchievementGroup}{current2.AchievementLevel}");
                    Response.AppendInteger(num);
                    Response.AppendInteger(Session.GetHabbo().GetAchievementData(current2.AchievementGroup) != null
                        ? Session.GetHabbo().GetAchievementData(current2.AchievementGroup).Progress
                        : 0);
                    Response.AppendInteger(current2.GetAchievement() == null
                        ? 0
                        : current2.GetAchievement().Levels[current2.AchievementLevel].Requirement);

                    if (num != 2 && failLevel == -1)
                        failLevel = current2.Level;
                }

                Response.AppendInteger(0);

                if (current.Type == "citizenship" && current.Level == 4)
                {
                    Response.AppendInteger(2);
                    Response.AppendString("HABBO_CLUB_VIP_7_DAYS");
                    Response.AppendInteger(7);
                    Response.AppendString(current.Prize);
                    Response.AppendInteger(0);
                }
                else
                {
                    Response.AppendInteger(1);
                    Response.AppendString(current.Prize);
                    Response.AppendInteger(0);
                }
            }

            SendResponse();
        }

        /// <summary>
        ///     Completes the safety quiz.
        /// </summary>
        internal void CompleteSafteyQuiz()
        {
            Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_SafetyQuizGraduate", 1);
        }

        /// <summary>
        ///     Hotels the view countdown.
        /// </summary>
        internal void HotelViewCountdown()
        {
            string time = Request.GetString();
            DateTime date;
            DateTime.TryParse(time, out date);
            TimeSpan diff = date - DateTime.Now;
            Response.Init(PacketLibraryManager.SendRequest("HotelViewCountdownMessageComposer"));
            Response.AppendString(time);
            Response.AppendInteger(Convert.ToInt32(diff.TotalSeconds));
            SendResponse();
            Console.WriteLine(diff.TotalSeconds);
        }

        internal void FindMoreFriends()
        {
            KeyValuePair<RoomData, uint>[] allRooms = Yupi.GetGame().GetRoomManager().GetActiveRooms();
            if (allRooms != null)
            {
                Random rnd = new Random();
                RoomData randomRoom = allRooms[rnd.Next(allRooms.Length)].Key;
                SimpleServerMessageBuffer success = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("FindMoreFriendsSuccessMessageComposer"));
                if (randomRoom == null)
                {
                    success.AppendBool(false);
                    Session.SendMessage(success);
                    return;
                }
                success.AppendBool(true);
                Session.SendMessage(success);
                SimpleServerMessageBuffer roomFwd = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("RoomForwardMessageComposer"));
                roomFwd.AppendInteger(randomRoom.Id);
                Session.SendMessage(roomFwd);
            }
        }

        internal void HotelViewRequestBadge()
        {
            string name = Request.GetString();
            Dictionary<string, string> hotelViewBadges = Yupi.GetGame().GetHotelView().HotelViewBadges;
            if (!hotelViewBadges.ContainsKey(name))
                return;
            string badge = hotelViewBadges[name];
            Session.GetHabbo().GetBadgeComponent().GiveBadge(badge, true, Session, true);
        }

        internal void GetCameraPrice()
        {
            GetResponse().Init(PacketLibraryManager.SendRequest("SetCameraPriceMessageComposer"));
            GetResponse().AppendInteger(0); //credits
            GetResponse().AppendInteger(10); //duckets
            SendResponse();
        }

        internal void GetHotelViewHallOfFame()
        {
            string code = Request.GetString();
            GetResponse().Init(PacketLibraryManager.SendRequest("HotelViewHallOfFameMessageComposer"));
            GetResponse().AppendString(code);
            IEnumerable<HallOfFameElement> rankings = Yupi.GetGame().GetHallOfFame().Rankings.Where(e => e.Competition == code);
            GetResponse().StartArray();
            int rank = 1;
            foreach (HallOfFameElement element in rankings)
            {
                Habbo user = Yupi.GetHabboById(element.UserId);
                if (user == null) continue;

                GetResponse().AppendInteger(user.Id);
                GetResponse().AppendString(user.UserName);
                GetResponse().AppendString(user.Look);
                GetResponse().AppendInteger(rank);
                GetResponse().AppendInteger(element.Score);
                rank++;
                GetResponse().SaveArray();
            }
            GetResponse().EndArray();
            SendResponse();
        }
    }
}