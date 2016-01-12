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
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Achievements.Composers;
using Yupi.Game.Achievements.Factories;
using Yupi.Game.Achievements.Structs;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Users;
using Yupi.Game.Users.Subscriptions;
using Yupi.Messages;
using Yupi.Messages.Handlers;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Achievements
{
    /// <summary>
    ///     Class AchievementManager.
    /// </summary>
    public class AchievementManager
    {
        /// <summary>
        ///     The achievement data cached
        /// </summary>
        internal ServerMessage AchievementDataCached;

        /// <summary>
        ///     The achievements
        /// </summary>
        internal Dictionary<string, Achievement> Achievements;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AchievementManager" /> class.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="loadedAchs">The loaded achs.</param>
        internal AchievementManager(IQueryAdapter dbClient, out uint loadedAchs)
        {
            Achievements = new Dictionary<string, Achievement>();
            LoadAchievements(dbClient);
            loadedAchs = (uint) Achievements.Count;
        }

        /// <summary>
        ///     Loads the achievements.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void LoadAchievements(IQueryAdapter dbClient)
        {
            Achievements.Clear();

            AchievementLevelFactory.GetAchievementLevels(out Achievements, dbClient);

            AchievementDataCached =
                new ServerMessage(LibraryParser.OutgoingRequest("SendAchievementsRequirementsMessageComposer"));
            AchievementDataCached.AppendInteger(Achievements.Count);

            foreach (Achievement ach in Achievements.Values)
            {
                AchievementDataCached.AppendString(ach.GroupName.Replace("ACH_", string.Empty));
                AchievementDataCached.AppendInteger(ach.Levels.Count);

                for (uint i = 1; i < ach.Levels.Count + 1; i++)
                {
                    AchievementDataCached.AppendInteger(i);
                    AchievementDataCached.AppendInteger(ach.Levels[i].Requirement);
                }
            }

            AchievementDataCached.AppendInteger(0);
        }

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="message">The message.</param>
        internal void GetList(GameClient session, ClientMessage message)
            => session.SendMessage(AchievementListComposer.Compose(session, Achievements.Values.ToList()));

        /// <summary>
        ///     Tries the progress login achievements.
        /// </summary>
        /// <param name="session">The session.</param>
        internal void TryProgressLoginAchievements(GameClient session)
        {
            if (session.GetHabbo() == null)
                return;

            if (session.GetHabbo().Achievements.ContainsKey("ACH_Login"))
            {
                int daysBtwLastLogin = Yupi.GetUnixTimeStamp() - session.GetHabbo().PreviousOnline;

                if (daysBtwLastLogin >= 51840 && daysBtwLastLogin <= 112320)
                    ProgressUserAchievement(session, "ACH_Login", 1, true);

                return;
            }

            ProgressUserAchievement(session, "ACH_Login", 1, true);
        }

        /// <summary>
        ///     Tries the progress registration achievements.
        /// </summary>
        /// <param name="session">The session.</param>
        internal void TryProgressRegistrationAchievements(GameClient session)
        {
            if (session.GetHabbo() == null)
                return;

            if (session.GetHabbo().Achievements.ContainsKey("ACH_RegistrationDuration"))
            {
                UserAchievement regAch = session.GetHabbo().GetAchievementData("ACH_RegistrationDuration");

                if (regAch.Level == 5)
                    return;

                double sinceMember = Yupi.GetUnixTimeStamp() - (int) session.GetHabbo().CreateDate;

                uint daysSinceMember = Convert.ToUInt32(Math.Round(sinceMember/86400));

                if (daysSinceMember == regAch.Progress)
                    return;

                uint days = daysSinceMember - regAch.Progress;

                if (days < 1)
                    return;

                ProgressUserAchievement(session, "ACH_RegistrationDuration", days);

                return;
            }

            ProgressUserAchievement(session, "ACH_RegistrationDuration", 1, true);
        }

        /// <summary>
        ///     Tries the progress habbo club achievements.
        /// </summary>
        /// <param name="session">The session.</param>
        internal void TryProgressHabboClubAchievements(GameClient session)
        {
            if (session.GetHabbo() == null || !session.GetHabbo().GetSubscriptionManager().HasSubscription)
                return;

            if (session.GetHabbo().Achievements.ContainsKey("ACH_VipHC"))
            {
                UserAchievement clubAch = session.GetHabbo().GetAchievementData("ACH_VipHC");

                if (clubAch.Level == 5)
                    return;

                Subscription subscription = session.GetHabbo().GetSubscriptionManager().GetSubscription();

                int sinceActivation = Yupi.GetUnixTimeStamp() - subscription.ActivateTime;

                if (sinceActivation < 31556926)
                    return;

                if (sinceActivation >= 31556926)
                {
                    ProgressUserAchievement(session, "ACH_VipHC", 1);
                    ProgressUserAchievement(session, "ACH_BasicClub", 1);
                }

                if (sinceActivation >= 63113851)
                {
                    ProgressUserAchievement(session, "ACH_VipHC", 1);
                    ProgressUserAchievement(session, "ACH_BasicClub", 1);
                }

                if (sinceActivation >= 94670777)
                {
                    ProgressUserAchievement(session, "ACH_VipHC", 1);
                    ProgressUserAchievement(session, "ACH_BasicClub", 1);
                }

                if (sinceActivation >= 126227704)
                {
                    ProgressUserAchievement(session, "ACH_VipHC", 1);
                    ProgressUserAchievement(session, "ACH_BasicClub", 1);
                }

                if (sinceActivation >= 157784630)
                {
                    ProgressUserAchievement(session, "ACH_VipHC", 1);
                    ProgressUserAchievement(session, "ACH_BasicClub", 1);
                }

                return;
            }

            ProgressUserAchievement(session, "ACH_VipHC", 1, true);
            ProgressUserAchievement(session, "ACH_BasicClub", 1, true);
        }

        /// <summary>
        ///     Progresses the user achievement.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="achievementGroup">The achievement group.</param>
        /// <param name="progressAmount">The progress amount.</param>
        /// <param name="fromZero">if set to <c>true</c> [from zero].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool ProgressUserAchievement(GameClient session, string achievementGroup, uint progressAmount,
            bool fromZero = false)
        {
            if (Achievements.ContainsKey(achievementGroup) && session?.GetHabbo() != null)
            {
                Achievement achievement = Achievements[achievementGroup];

                Habbo user = session.GetHabbo();

                // Get UserAchievementData, if the user doesn't has the Achievement, create a new.
                UserAchievement userAchievement = user.Achievements.ContainsKey(achievementGroup)
                    ? user.GetAchievementData(achievementGroup)
                    : new UserAchievement(achievementGroup, 0, 0);

                // If is a New Achievement is fromZero
                if (!user.Achievements.ContainsKey(achievementGroup))
                    fromZero = true;

                // If user hasn't the Achievement, after created the new, Must add in Collections.
                if (!user.Achievements.ContainsKey(achievementGroup))
                    user.Achievements.Add(achievementGroup, userAchievement);

                // Get Achievement 
                userAchievement = user.Achievements[achievementGroup];

                // Total Levels from this Achievement
                uint achievementLevelsCount = (uint) achievement.Levels.Count;

                // Get User Achievement Level
                uint achievementCurrentLevel = userAchievement.Level;

                // Get User Achievement Progress
                uint achievementCurrentProgress = userAchievement.Progress;

                // If the next Level is the last level must set to Levels.Count (Ex: 38 Levels => .Count = 37 (Max Level in the Array, but .Count 37 == 38, Soo need put Level - 1)
                uint achievementNextLevel = achievementCurrentLevel + 1 > achievementLevelsCount
                    ? achievementLevelsCount
                    : achievementCurrentLevel + 1;

                // Set Achievement Progress
                uint achievementProgress = achievementCurrentProgress + progressAmount;

                // If he has already the Max, something is wrong.
                if (achievementCurrentLevel == achievementLevelsCount)
                    return false;

                // Get Next Level Data
                AchievementLevel achievementNextLevelData = achievement.Levels[achievementNextLevel];

                // if progress isn't sufficient or, isn't new Achievement
                if (achievementProgress < achievementNextLevelData.Requirement || achievementCurrentLevel >= 1)
                    fromZero = false;

                // If progress is sufficient to next level, or is new Achievement
                if (achievementProgress >= achievementNextLevelData.Requirement || (achievementCurrentLevel < 1))
                    fromZero = true;

                // if is a new level (but level isn't 0)
                if (achievementProgress >= achievementNextLevelData.Requirement)
                    achievementProgress = 0;

                // If is new Level
                if (fromZero)
                {
                    // Set Level
                    userAchievement.SetLevel(achievementNextLevel);

                    // Set Progress
                    userAchievement.SetProgress(achievementProgress);

                    // Give Reward Points
                    user.AchievementPoints += achievementNextLevelData.RewardPoints;
                    user.NotifyNewPixels(achievementNextLevelData.RewardPixels);
                    user.Duckets += achievementNextLevelData.RewardPixels;

                    // Update Points Balance
                    user.UpdateActivityPointsBalance();

                    // Remove old Badge - (Is not problem if is First Level Badge, because if the user hasn't the badg, simply, will not remove.
                    user.GetBadgeComponent()
                        .RemoveBadge(Convert.ToString($"{achievementGroup}{achievementNextLevel - 1}"), session);

                    // Give new Badge
                    user.GetBadgeComponent().GiveBadge($"{achievementGroup}{achievementNextLevel}", true, session);

                    // Update in Database
                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        commitableQueryReactor.RunFastQuery(
                            $"REPLACE INTO users_achievements VALUES ('{user.Id}', '{achievementGroup}', '{achievementNextLevel}', '{achievementProgress}')");

                    // Send Unlocked Composer
                    session.SendMessage(AchievementUnlockedComposer.Compose(achievement, achievementNextLevel,
                        achievementNextLevelData.RewardPoints, achievementNextLevelData.RewardPixels));

                    // Send Score Composer
                    session.SendMessage(AchievementScoreUpdateComposer.Compose(user.AchievementPoints));

                    // Send Progress Composer
                    session.SendMessage(AchievementProgressComposer.Compose(achievement, achievementNextLevel,
                        achievementNextLevelData, achievementLevelsCount, userAchievement));

                    // Set Talent
                    if (
                        Yupi.GetGame()
                            .GetTalentManager()
                            .Talents.Values.Any(talent => talent.AchievementGroup == achievementGroup))
                        Yupi.GetGame()
                            .GetTalentManager()
                            .CompleteUserTalent(session,
                                Yupi.GetGame().GetTalentManager().GetTalentData(achievementGroup));
                }
                else
                {
                    // Get Current Level Data
                    AchievementLevel achievementCurrentLevelData = achievement.Levels[achievementCurrentLevel];

                    // It's the Same Level
                    userAchievement.SetLevel(achievementCurrentLevel);

                    // But increase Progress
                    userAchievement.SetProgress(achievementProgress);

                    // Update in Database
                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        commitableQueryReactor.RunFastQuery(
                            $"REPLACE INTO users_achievements VALUES ('{user.Id}', '{achievementGroup}', '{achievementCurrentLevel}', '{achievementProgress}')");

                    // Compose Current Data
                    session.SendMessage(AchievementProgressComposer.Compose(achievement, achievementCurrentLevel,
                        achievementCurrentLevelData, achievementLevelsCount, userAchievement));
                }

                // Send User New Data
                GameClientMessageHandler messageHandler = session.GetMessageHandler();

                messageHandler.GetResponse().Init(LibraryParser.OutgoingRequest("UpdateUserDataMessageComposer"));
                messageHandler.GetResponse().AppendInteger(-1);
                messageHandler.GetResponse().AppendString(user.Look);
                messageHandler.GetResponse().AppendString(user.Gender.ToLower());
                messageHandler.GetResponse().AppendString(user.Motto);
                messageHandler.GetResponse().AppendInteger(user.AchievementPoints);

                messageHandler.SendResponse();

                return true;
            }

            return false;
        }

        /// <summary>
        ///     Gets the achievement.
        /// </summary>
        /// <param name="achievementGroup">The achievement group.</param>
        /// <returns>Achievement.</returns>
        internal Achievement GetAchievement(string achievementGroup)
            => Achievements.ContainsKey(achievementGroup) ? Achievements[achievementGroup] : null;
    }
}