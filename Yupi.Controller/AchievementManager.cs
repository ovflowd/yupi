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
using System.Linq;
using Yupi.Messages.Contracts;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;

namespace Yupi.Controller
{
    public class AchievementManager
    {
        private readonly IRepository<Achievement> AchievementRepository;
        public IDictionary<string, Achievement> Achievements;
        private readonly ClientManager ClientManager;
        private readonly IRepository<UserInfo> UserRepository;

        public AchievementManager()
        {
            AchievementRepository = DependencyFactory.Resolve<IRepository<Achievement>>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            LoadAchievements();
        }

        private void LoadAchievements()
        {
            Achievements = AchievementRepository.All().ToDictionary(x => x.GroupName);
        }

        public void TryProgressLoginAchievements(Habbo session)
        {
            // FIXME
            /*
            if (session.GetHabbo ().Achievements.ContainsKey ("ACH_Login")) {
                int daysBtwLastLogin = Yupi.GetUnixTimeStamp () - session.Info.PreviousOnline;

                if (daysBtwLastLogin >= 51840 && daysBtwLastLogin <= 112320)
                    ProgressUserAchievement (session, "ACH_Login", );

                return;
            }

            ProgressUserAchievement (session, "ACH_Login", 1);
            */
        }

        public void TryProgressRegistrationAchievements(Habbo session)
        {
            // FIXME

            /*
            if (session.GetHabbo () == null)
                return;

            if (session.GetHabbo ().Achievements.ContainsKey ("ACH_RegistrationDuration")) {
                UserAchievement regAch = session.GetHabbo ().GetAchievementData ("ACH_RegistrationDuration");

                if (regAch.Level == 5)
                    return;

                double sinceMember = Yupi.GetUnixTimeStamp () - (int)session.GetHabbo ().CreateDate;

                uint daysSinceMember = Convert.ToUInt32 (Math.Round (sinceMember / 86400));

                if (daysSinceMember == regAch.Progress)
                    return;

                uint days = daysSinceMember - regAch.Progress;

                if (days < 1)
                    return;

                ProgressUserAchievement (session, "ACH_RegistrationDuration", days);

                return;
            }

            ProgressUserAchievement (session, "ACH_RegistrationDuration", 1, true);
            */
        }

        public void TryProgressHabboClubAchievements(Habbo session)
        {
            if (!session.Info.Subscription.IsValid())
                return;

            /*
            if (session.GetHabbo ().Achievements.ContainsKey ("ACH_VipHC")) {
                UserAchievement clubAch = session.GetHabbo ().GetAchievementData ("ACH_VipHC");

                if (clubAch.Level == 5)
                    return;

                Subscription subscription = session.Info.Subscription;

                TimeSpan sinceActivation = DateTime.Now - subscription.ActivateTime;

                int years = sinceActivation.TotalDays / 365;

                if (sinceActivation.TotalDays >= 1) {
                    ProgressUserAchievement (session, "ACH_VipHC", 1);
                    ProgressUserAchievement (session, "ACH_BasicClub", 1);
                }

                if (sinceActivation.TotalDays >= 2) {
                    ProgressUserAchievement (session, "ACH_VipHC", 1);
                    ProgressUserAchievement (session, "ACH_BasicClub", 1);
                }

                if (sinceActivation.TotalDays >= 3) {
                    ProgressUserAchievement (session, "ACH_VipHC", 1);
                    ProgressUserAchievement (session, "ACH_BasicClub", 1);
                }

                if (sinceActivation.TotalDays >= 4) {
                    ProgressUserAchievement (session, "ACH_VipHC", 1);
                    ProgressUserAchievement (session, "ACH_BasicClub", 1);
                }

                if (sinceActivation.TotalDays >= 5) {
                    ProgressUserAchievement (session, "ACH_VipHC", 1);
                    ProgressUserAchievement (session, "ACH_BasicClub", 1);
                }

                return;
            }

            ProgressUserAchievement (session, "ACH_VipHC", 1, true);
            ProgressUserAchievement (session, "ACH_BasicClub", 1, true);
            */
            // FIXME
        }

        // TODO Enum?
        public bool ProgressUserAchievement(UserInfo info, string achievementGroup, int progressAmount)
        {
            var session = ClientManager.GetByInfo(info);
            return ProgressUserAchievement(info, achievementGroup, progressAmount, session);
        }

        public bool ProgressUserAchievement(Habbo user, string achievementGroup, int progressAmount)
        {
            return ProgressUserAchievement(user.Info, achievementGroup, progressAmount, user);
        }

        private bool ProgressUserAchievement(UserInfo user, string achievementGroup, int progressAmount, Habbo session)
        {
            if (Achievements.ContainsKey(achievementGroup))
            {
                var achievement = Achievements[achievementGroup];

                var userAchievement = user.Achievements.Single(x => x.Achievement.GroupName == achievementGroup);

                if (userAchievement == null)
                {
                    userAchievement = new UserAchievement
                    {
                        Achievement = achievement,
                        Level = achievement.DefaultLevel()
                    };

                    user.Achievements.Add(userAchievement);
                }

                userAchievement.Progress += progressAmount;

                // If is new Level
                if (userAchievement.CanIncreaseLevel())
                {
                    userAchievement.IncreaseLevel();

                    // Give Reward Points
                    user.Wallet.AchievementPoints += userAchievement.Level.RewardPoints;
                    user.Wallet.Duckets += userAchievement.Level.RewardPixels;

                    user.Badges.RemoveBadge(userAchievement.Achievement.GroupName + (userAchievement.Level.Level - 1));

                    // Give new Badge
                    user.Badges.GiveBadge(achievementGroup + userAchievement.Level.Level);

                    if (session != null)
                    {
                        session.Router.GetComposer<ActivityPointsMessageComposer>().Compose(session, user.Wallet);

                        // Send Unlocked Composer
                        session.Router.GetComposer<UnlockAchievementMessageComposer>().Compose(session,
                            achievement, userAchievement.Level.Level,
                            userAchievement.Level.RewardPoints, userAchievement.Level.RewardPixels);

                        // Send Score Composer
                        session.Router.GetComposer<AchievementPointsMessageComposer>()
                            .Compose(session, user.Wallet.AchievementPoints);
                    }
                    // TODO Reimplement talents properly
                }

                UserRepository.Save(user);

                if (session != null)
                {
                    session.Router.GetComposer<AchievementProgressMessageComposer>().Compose(session, userAchievement);
                    session.Router.GetComposer<UpdateUserDataMessageComposer>().Compose(session, user);
                }

                return true;
            }

            return false;
        }

        public Achievement GetAchievement(string achievementGroup)
        {
            return Achievements[achievementGroup];
        }
    }
}