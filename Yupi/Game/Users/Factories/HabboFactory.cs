using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Game.Browser.Models;
using Yupi.Game.Groups.Structs;

namespace Yupi.Game.Users.Factories
{
    /// <summary>
    ///     Class HabboFactory.
    /// </summary>
    internal static class HabboFactory
    {
        /// <summary>
        ///     Generates the habbo.
        /// </summary>
        /// <param name="dRow">The d row.</param>
        /// <param name="mRow">The m row.</param>
        /// <param name="group">The group.</param>
        /// <returns>Habbo.</returns>
        internal static Habbo GenerateHabbo(DataRow dRow, DataRow mRow, HashSet<GroupMember> group)
        {
            Dictionary<int, UserSearchLog> navilogs = new Dictionary<int, UserSearchLog>();

            #region User Basic Data

            // Positive Integers (Unsigned)
            uint id = (uint) dRow["id"];
            uint ras = (uint) dRow["rank"];
            uint homeRoom = (uint) dRow["home_room"];
            uint credits = (uint) dRow["credits"];
            uint activityPoints = (uint) dRow["activity_points"];
            uint diamonds = (uint) dRow["diamonds"];

            // Strings
            string userName = dRow["username"].ToString();
            string realName = dRow["real_name"].ToString();
            string motto = dRow["motto"].ToString();
            string look = dRow["look"].ToString();
            string gender = dRow["gender"].ToString();
            string citizenship = dRow["talent_status"].ToString();

            // Integers
            int lastOnline = (int) dRow["last_online"];
            int createDate = (int) dRow["account_created"];
            int lastChange = (int) dRow["last_name_change"];
            int regTimestamp = (int) dRow["account_created"];
            int tradeLockExpire = (int) dRow["trade_lock_expire"];
            int buildersExpire = (int) dRow["builders_expire"];
            int buildersItemsMax = (int) dRow["builders_items_max"];
            int buildersItemsUsed = (int) dRow["builders_items_used"];
            int releaseVersion = (int) dRow["release_version"];
            int dutyLevel = (int) dRow["duty_level"];

            // Booleans (Enumerators/ String Enumerators)
            bool hasFriendRequestsDisabled = Yupi.EnumToBool(dRow["block_newfriends"].ToString());
            bool appearOffline = Yupi.EnumToBool(dRow["hide_online"].ToString());
            bool hideInRoom = Yupi.EnumToBool(dRow["hide_inroom"].ToString());
            bool muted = Yupi.EnumToBool(dRow["is_muted"].ToString());
            bool vip = Yupi.EnumToBool(dRow["vip"].ToString());
            bool online = Yupi.EnumToBool(dRow["online"].ToString());
            bool tradeLocked = Yupi.EnumToBool(dRow["trade_lock"].ToString());
            bool nuxPassed = Yupi.EnumToBool(dRow["nux_passed"].ToString());
            bool onDuty = Yupi.EnumToBool(dRow["on_duty"].ToString());

            // Double Integers
            double lastActivityPointsUpdate = (double) dRow["activity_points_lastupdate"];

            #endregion

            #region User Status and Additional Data

            // Integers
            int respect = (int) mRow["respect"];
            int dailyRespectPoints = (int) mRow["daily_respect_points"];
            int dailyPetRespectPoints = (int) mRow["daily_pet_respect_points"];
            int currentQuestId = 0; //(uint)mRow["quest_id"];
            int currentQuestProgress = (int) mRow["quest_progress"];
            int favId = (int) mRow["favourite_group"];
            int dailyCompetitionVotes = (int) mRow["daily_competition_votes"];

            // Positive Integers (Unsigned)
            uint achievementPoints = (uint) mRow["achievement_score"];

            #endregion

            #region Navigator Logs

            // Navigator Search Logs Query String
            string navilogstring = dRow["navigator_logs"].ToString();

            // Navigator Logs Builder
            if (navilogstring.Length > 0)
                foreach (
                    UserSearchLog naviLogs in
                        navilogstring.Split(';')
                            .Where(value => navilogstring.Contains(','))
                            .Select(
                                value =>
                                    new UserSearchLog(int.Parse(value.Split(',')[0]), value.Split(',')[1],
                                        value.Split(',')[2]))
                            .Where(naviLogs => !navilogs.ContainsKey(naviLogs.Id)))
                    navilogs.Add(naviLogs.Id, naviLogs);

            #endregion

            #region Return Generated Data

            // Return new Generated Habbo Model
            return new Habbo(id, userName, realName, ras, motto, look, gender, credits, activityPoints,
                lastActivityPointsUpdate, muted, homeRoom, respect, dailyRespectPoints, dailyPetRespectPoints,
                hasFriendRequestsDisabled, currentQuestId, currentQuestProgress, achievementPoints, regTimestamp,
                lastOnline, appearOffline, hideInRoom, vip, createDate, online, citizenship, diamonds, group, favId,
                lastChange, tradeLocked, tradeLockExpire, nuxPassed, buildersExpire, buildersItemsMax,
                buildersItemsUsed, releaseVersion, onDuty, navilogs, dailyCompetitionVotes, dutyLevel);

            #endregion
        }
    }
}