using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Sessions.Interfaces;
using Yupi.Game.Achievements.Structs;
using Yupi.Game.Catalogs;
using Yupi.Game.Groups.Interfaces;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pets;
using Yupi.Game.RoomBots;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Users.Badges.Models;
using Yupi.Game.Users.Data.Models;
using Yupi.Game.Users.Inventory;
using Yupi.Game.Users.Messenger.Structs;
using Yupi.Game.Users.Relationships;
using Yupi.Game.Users.Subscriptions;

namespace Yupi.Game.Users.Factories
{
    /// <summary>
    ///     Class UserDataFactory.
    /// </summary>
    internal class UserDataFactory
    {
        /// <summary>
        ///     Gets the user data.
        /// </summary>
        /// <param name="sessionTicket">The session ticket.</param>
        /// <param name="errorCode">The error code.</param>
        /// <returns>UserData.</returns>
        internal static UserData GetUserData(string sessionTicket, out uint errorCode)
        {
            errorCode = 1;

            DataTable groupsTable;
            DataRow dataRow;
            DataTable achievementsTable;
            DataTable talentsTable;
            DataRow statsTable;
            DataTable favoritesTable;
            DataTable ignoresTable;
            DataTable tagsTable;
            DataRow subscriptionsRow;
            DataTable badgesTable;
            DataTable itemsTable;
            DataTable effectsTable;
            DataTable pollsTable;
            DataTable friendsTable;
            DataTable friendsRequestsTable;

            DataTable relationShipsTable;
            DataTable botsTable;
            DataTable questsTable;
            DataTable petsTable;

            DataTable myRoomsTable;

            uint userId;
            string userName, userLook;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                // Get User Data
                queryReactor.SetQuery("SELECT * FROM users WHERE auth_ticket = @ticket");

                // Execute User Data Query
                queryReactor.AddParameter("ticket", sessionTicket);
                dataRow = queryReactor.GetRow();

                if (dataRow == null)
                    return null;

                userId = (uint)dataRow["id"];
                userName = dataRow["username"].ToString();
                userLook = dataRow["look"].ToString();

                int regDate = (int) dataRow["account_created"] == 0 ? Yupi.GetUnixTimeStamp() : (int) dataRow["account_created"];

                // Check Register Date
                if ((int) dataRow["account_created"] == 0)
                    queryReactor.RunFastQuery($"UPDATE users SET account_created = {regDate} WHERE id = {userId}");

                // Disconnect if user Already Logged-in, Doesn't need check. If user isn't logged, nothing will happen.
                if (Yupi.GetGame().GetClientManager().GetClientByUserId(userId) != null)
                    Yupi.GetGame().GetClientManager().GetClientByUserId(userId)?.Disconnect("User connected in other place");

                // Update User statusses
                queryReactor.RunFastQuery($"UPDATE users SET online = 1 WHERE id = {userId};" +
                                          $"REPLACE INTO users_info(user_id, login_timestamp) VALUES('{userId}', '{Yupi.GetUnixTimeStamp()}');" +
                                          $"REPLACE INTO users_stats (id) VALUES ('{userId}');");

                // Get User Achievements Data
                queryReactor.SetQuery($"SELECT * FROM users_achievements WHERE userid = {userId}");
                achievementsTable = queryReactor.GetTable();

                // Get User Talent Data
                queryReactor.SetQuery($"SELECT * FROM users_talents WHERE userid = {userId}");
                talentsTable = queryReactor.GetTable();        

                // Get User Favorite Room
                queryReactor.SetQuery($"SELECT room_id FROM users_favorites WHERE user_id = {userId}");
                favoritesTable = queryReactor.GetTable();

                // Get User Ignored Users
                queryReactor.SetQuery($"SELECT ignore_id FROM users_ignores WHERE user_id = {userId}");
                ignoresTable = queryReactor.GetTable();

                // Get User Tags
                queryReactor.SetQuery($"SELECT tag FROM users_tags WHERE user_id = {userId}");
                tagsTable = queryReactor.GetTable();

                // Get User Subscriptions
                queryReactor.SetQuery($"SELECT * FROM users_subscriptions WHERE user_id = {userId} AND timestamp_expire > UNIX_TIMESTAMP() ORDER BY subscription_id DESC LIMIT 1");
                subscriptionsRow = queryReactor.GetRow();

                // Get User Badges
                queryReactor.SetQuery($"SELECT * FROM users_badges WHERE user_id = {userId}");
                badgesTable = queryReactor.GetTable();

                // Get User Inventory Items
                queryReactor.SetQuery($"SELECT items_rooms.*, COALESCE(items_groups.group_id, 0) AS group_id FROM items_rooms LEFT OUTER JOIN items_groups ON items_rooms.id = items_groups.id WHERE room_id = 0 AND user_id={userId} LIMIT 8000");
                itemsTable = queryReactor.GetTable();

                // Get user Effects
                queryReactor.SetQuery($"SELECT * FROM users_effects WHERE user_id = {userId}");
                effectsTable = queryReactor.GetTable();

                // Get User Polls
                queryReactor.SetQuery($"SELECT poll_id FROM users_polls WHERE user_id = {userId} GROUP BY poll_id;");
                pollsTable = queryReactor.GetTable();

                // Get User Friends
                queryReactor.SetQuery($"SELECT users.* FROM users JOIN messenger_friendships ON users.id = messenger_friendships.user_one_id WHERE messenger_friendships.user_two_id = {userId} UNION ALL SELECT users.* FROM users JOIN messenger_friendships ON users.id = messenger_friendships.user_two_id WHERE messenger_friendships.user_one_id = {userId}");
                friendsTable = queryReactor.GetTable();

                // Get User Stats
                queryReactor.SetQuery($"SELECT * FROM users_stats WHERE id = {userId}");
                statsTable = queryReactor.GetRow();

                // Get User Friends Requests
                queryReactor.SetQuery($"SELECT messenger_requests.*,users.* FROM users JOIN messenger_requests ON users.id = messenger_requests.from_id WHERE messenger_requests.to_id = {userId}");
                friendsRequestsTable = queryReactor.GetTable();

                // Get User Rooms Data
                queryReactor.SetQuery($"SELECT * FROM rooms_data WHERE owner = '{userId}' LIMIT 150");
                myRoomsTable = queryReactor.GetTable();

                // Get User Pets Data
                queryReactor.SetQuery($"SELECT * FROM bots_data WHERE user_id = {userId} AND room_id = 0 AND ai_type='pet'");
                petsTable = queryReactor.GetTable();

                // Get User Quests Data
                queryReactor.SetQuery($"SELECT * FROM users_quests_data WHERE user_id = {userId}");
                questsTable = queryReactor.GetTable();

                // Get User Bots Data
                queryReactor.SetQuery($"SELECT * FROM bots_data WHERE user_id = {userId} AND room_id=0 AND ai_type='generic'");
                botsTable = queryReactor.GetTable();

                // Get User Groups Data
                queryReactor.SetQuery($"SELECT * FROM groups_members WHERE user_id = {userId}");
                groupsTable = queryReactor.GetTable();

                // Get User Relationships Data
                queryReactor.SetQuery($"SELECT * FROM users_relationships WHERE user_id = {userId}");
                relationShipsTable = queryReactor.GetTable();
            }

            Dictionary<string, UserAchievement> achievements = new Dictionary<string, UserAchievement>();

            foreach (DataRow row in achievementsTable.Rows)
            {
                string text = (string) row["group"];
                int level = (int) row["level"];
                int progress = (int) row["progress"];

                achievements.Add(text, new UserAchievement(text, level, progress));
            }

            Dictionary<int, UserTalent> talents = new Dictionary<int, UserTalent>();

            foreach (DataRow row in talentsTable.Rows)
            {
                int num2 = (int) row["talent_id"];
                int state = (int) row["talent_state"];

                talents.Add(num2, new UserTalent(num2, state));
            }

            List<uint> favorites = favoritesTable.Rows.Cast<DataRow>().Select(row => (uint) row["room_id"]).ToList();
            List<uint> ignoreUsers = ignoresTable.Rows.Cast<DataRow>().Select(row => (uint) row["ignore_id"]).ToList();
            List<string> tags = tagsTable.Rows.Cast<DataRow>().Select(row => row["tag"].ToString().Replace(" ", "")).ToList();

            Dictionary<uint, RoomBot> inventoryBots = botsTable.Rows.Cast<DataRow>().Select(BotManager.GenerateBotFromRow).ToDictionary(roomBot => roomBot.BotId);

            List<Badge> badges = badgesTable.Rows.Cast<DataRow>().Select(dataRow8 => new Badge((string) dataRow8["badge_id"], (int) dataRow8["badge_slot"])).ToList();

            Subscription subscriptions = null;

            if (subscriptionsRow != null)
                subscriptions = new Subscription((int) subscriptionsRow["subscription_id"],
                    (int) subscriptionsRow["timestamp_activated"], (int) subscriptionsRow["timestamp_expire"],
                    (int) subscriptionsRow["timestamp_lastgift"]);

            List<UserItem> items = (from DataRow row in itemsTable.Rows
                let id = Convert.ToUInt32(row["id"])
                let itemName = row["item_name"].ToString()
                where Yupi.GetGame().GetItemManager().ContainsItemByName(itemName)
                let extraData = !DBNull.Value.Equals(row[4]) ? (string) row[4] : string.Empty
                let theGroup = Convert.ToUInt32(row["group_id"])
                let songCode = (string) row["songcode"]
                select new UserItem(id, itemName, extraData, theGroup, songCode)).ToList();

            List<AvatarEffect> effects = (from DataRow row in effectsTable.Rows
                let effectId = (int) row["effect_id"]
                let totalDuration = (int) row["total_duration"]
                let activated = Yupi.EnumToBool((string) row["is_activated"])
                let activateTimestamp = (double) row["activated_stamp"]
                let type = Convert.ToInt16(row["type"])
                select new AvatarEffect(effectId, totalDuration, activated, activateTimestamp, type)).ToList();

            HashSet<uint> pollSuggested = new HashSet<uint>();

            foreach (uint pId in pollsTable.Rows.Cast<DataRow>().Select(row => (uint) row["poll_id"]))
                pollSuggested.Add(pId);

            Dictionary<uint, MessengerBuddy> friends = new Dictionary<uint, MessengerBuddy>();

            foreach (DataRow row in friendsTable.Rows)
            {
                uint num4 = (uint) row["id"];
                string pUsername = (string) row["username"];
                string pLook = (string) row["look"];
                string pMotto = (string) row["motto"];
                bool pAppearOffline = Yupi.EnumToBool(row["hide_online"].ToString());
                bool pHideInroom = Yupi.EnumToBool(row["hide_inroom"].ToString());

                if (!Equals(num4, userId) && !friends.ContainsKey(num4))
                    friends.Add(num4, new MessengerBuddy(num4, pUsername, pLook, pMotto, pAppearOffline, pHideInroom));
            }

            Dictionary<uint, MessengerRequest> friendsRequests = new Dictionary<uint, MessengerRequest>();

            foreach (DataRow row in friendsRequestsTable.Rows)
            {
                uint num5 = Convert.ToUInt32(row["from_id"]);
                uint num6 = Convert.ToUInt32(row["to_id"]);
                string pUsername2 = row["username"].ToString();
                string pLook = row["look"].ToString();

                if (num5 != userId)
                {
                    if (!friendsRequests.ContainsKey(num5))
                        friendsRequests.Add(num5, new MessengerRequest(userId, num5, pUsername2, pLook));
                    else if (!friendsRequests.ContainsKey(num6))
                        friendsRequests.Add(num6, new MessengerRequest(userId, num6, pUsername2, pLook));
                }
            }

            HashSet<RoomData> myRooms = new HashSet<RoomData>();

            foreach (DataRow row in myRoomsTable.Rows)
                myRooms.Add(Yupi.GetGame().GetRoomManager().FetchRoomData((uint)row["id"], row));

            Dictionary<uint, Pet> pets = new Dictionary<uint, Pet>();

            foreach (DataRow row in petsTable.Rows)
            {
                using (IQueryAdapter queryreactor3 = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryreactor3.SetQuery($"SELECT * FROM pets_data WHERE id = {row["id"]} LIMIT 1");

                    DataRow row3 = queryreactor3.GetRow();

                    if (row3 == null)
                        continue;

                    pets.Add((uint)row["id"], CatalogManager.GeneratePetFromRow(row, row3));
                }
            }

            Dictionary<int, int> quests = new Dictionary<int, int>();

            foreach (DataRow row in questsTable.Rows)
            {
                int key = (int) row["quest_id"];
                int value3 = (int) row["progress"];

                if (quests.ContainsKey(key))
                    quests.Remove(key);

                quests.Add(key, value3);
            }

            HashSet<GroupMember> groups = new HashSet<GroupMember>();

            foreach (DataRow row in groupsTable.Rows)
                groups.Add(new GroupMember(userId, userName, userLook, (int) row["group_id"], Convert.ToInt16(row["rank"]), (int) row["date_join"]));

            Dictionary<int, Relationship> relationShips = relationShipsTable.Rows.Cast<DataRow>().ToDictionary(row => (int) row[0], row => new Relationship((int) row[0], (int) row[2], Convert.ToInt32(row[3].ToString())));

            Habbo user = HabboFactory.GenerateHabbo(dataRow, statsTable, groups);

            errorCode = 0;

            if (user.Rank >= Yupi.StaffAlertMinRank)
                friends.Add(0, new MessengerBuddy(0, "Staff Chat", "hr-831-45.fa-1206-91.sh-290-1331.ha-3129-100.hd-180-2.cc-3039-73.ch-3215-92.lg-270-73", string.Empty, false, true));
            else if (user.Rank >= Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]))
                friends.Add(0, new MessengerBuddy(0, "Ambassador Chat", "hr-831-45.fa-1206-91.sh-290-1331.ha-3129-100.hd-180-2.cc-3039-73.ch-3215-92.lg-270-73", string.Empty, false, true));

            return new UserData(userId, achievements, talents, favorites, ignoreUsers, tags, subscriptions, badges,
                items, effects, friends, friendsRequests, myRooms, pets, quests, user, inventoryBots, relationShips,
                pollSuggested, 0);
        }

        /// <summary>
        ///     Gets the user data.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>UserData.</returns>
        internal static UserData GetUserData(int userId)
        {
            DataRow dataRow;
            DataRow row;
            DataTable table;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery($"SELECT * FROM users WHERE id = {userId} LIMIT 1");

                dataRow = queryReactor.GetRow();

                Yupi.GetGame().GetClientManager().LogClonesOut((uint)userId);

                if (dataRow == null)
                    return null;

                if (Yupi.GetGame().GetClientManager().GetClientByUserId((uint)userId) != null)
                    return null;

                queryReactor.SetQuery($"SELECT * FROM groups_members WHERE user_id={userId}");
                queryReactor.GetTable();

                queryReactor.SetQuery($"SELECT * FROM users_stats WHERE id = {userId}");
                row = queryReactor.GetRow();

                queryReactor.SetQuery($"SELECT * FROM users_relationships WHERE user_id={userId}");
                table = queryReactor.GetTable();
            }

            Dictionary<string, UserAchievement> achievements = new Dictionary<string, UserAchievement>();
            Dictionary<int, UserTalent> talents = new Dictionary<int, UserTalent>();
            List<uint> favouritedRooms = new List<uint>();
            List<uint> ignores = new List<uint>();
            List<string> tags = new List<string>();
            List<Badge> badges = new List<Badge>();
            List<UserItem> inventory = new List<UserItem>();
            List<AvatarEffect> effects = new List<AvatarEffect>();
            Dictionary<uint, MessengerBuddy> friends = new Dictionary<uint, MessengerBuddy>();
            Dictionary<uint, MessengerRequest> requests = new Dictionary<uint, MessengerRequest>();
            HashSet<RoomData> rooms = new HashSet<RoomData>();
            Dictionary<uint, Pet> pets = new Dictionary<uint, Pet>();
            Dictionary<int, int> quests = new Dictionary<int, int>();
            Dictionary<uint, RoomBot> bots = new Dictionary<uint, RoomBot>();
            HashSet<GroupMember> group = new HashSet<GroupMember>();
            HashSet<uint> pollData = new HashSet<uint>();

            Dictionary<int, Relationship> dictionary = table.Rows.Cast<DataRow>().ToDictionary(dataRow2 => (int) dataRow2["id"], dataRow2 => new Relationship((int) dataRow2["id"], (int) dataRow2["target"], Convert.ToInt32(dataRow2["type"].ToString())));

            Habbo user = HabboFactory.GenerateHabbo(dataRow, row, group);

            return new UserData((uint)userId, achievements, talents, favouritedRooms, ignores, tags, null, badges, inventory,
                effects, friends, requests, rooms, pets, quests, user, bots, dictionary, pollData, 0);
        }
    }
}