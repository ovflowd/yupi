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
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Messages.Contracts;
using Yupi.Model.Domain;
using Yupi.Model;

namespace Yupi.Controller
{
	public class AchievementManager
	{
		private IDictionary<string, Achievement> Achievements;

		private Repository<Achievement> Repository;

		public AchievementManager ()
		{
			Repository = DependencyFactory.Resolve<Repository<Achievement>> ();
			LoadAchievements ();
		}

		private void LoadAchievements ()
		{
			Achievements = Repository.All ().ToDictionary ((x) => x.GroupName);
		}
			
		public void TryProgressLoginAchievements ()
		{
			throw new NotImplementedException ();
			/*
			if (session.GetHabbo () == null)
				return;

			if (session.GetHabbo ().Achievements.ContainsKey ("ACH_Login")) {
				int daysBtwLastLogin = Yupi.GetUnixTimeStamp () - session.GetHabbo ().PreviousOnline;

				if (daysBtwLastLogin >= 51840 && daysBtwLastLogin <= 112320)
					ProgressUserAchievement (session, "ACH_Login", 1, true);

				return;
			}

			ProgressUserAchievement (session, "ACH_Login", 1, true);
			*/
		}
			
		public void TryProgressRegistrationAchievements ()
		{
			throw new NotImplementedException ();

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
			
		public void TryProgressHabboClubAchievements ()
		{
			throw new NotImplementedException ();
			/*
			if (session.GetHabbo () == null || !session.GetHabbo ().GetSubscriptionManager ().HasSubscription)
				return;

			if (session.GetHabbo ().Achievements.ContainsKey ("ACH_VipHC")) {
				UserAchievement clubAch = session.GetHabbo ().GetAchievementData ("ACH_VipHC");

				if (clubAch.Level == 5)
					return;

				Subscription subscription = session.GetHabbo ().GetSubscriptionManager ().GetSubscription ();

				int sinceActivation = Yupi.GetUnixTimeStamp () - subscription.ActivateTime;

				if (sinceActivation < 31556926)
					return;

				if (sinceActivation >= 31556926) {
					ProgressUserAchievement (session, "ACH_VipHC", 1);
					ProgressUserAchievement (session, "ACH_BasicClub", 1);
				}

				if (sinceActivation >= 63113851) {
					ProgressUserAchievement (session, "ACH_VipHC", 1);
					ProgressUserAchievement (session, "ACH_BasicClub", 1);
				}

				if (sinceActivation >= 94670777) {
					ProgressUserAchievement (session, "ACH_VipHC", 1);
					ProgressUserAchievement (session, "ACH_BasicClub", 1);
				}

				if (sinceActivation >= 126227704) {
					ProgressUserAchievement (session, "ACH_VipHC", 1);
					ProgressUserAchievement (session, "ACH_BasicClub", 1);
				}

				if (sinceActivation >= 157784630) {
					ProgressUserAchievement (session, "ACH_VipHC", 1);
					ProgressUserAchievement (session, "ACH_BasicClub", 1);
				}

				return;
			}

			ProgressUserAchievement (session, "ACH_VipHC", 1, true);
			ProgressUserAchievement (session, "ACH_BasicClub", 1, true);
			*/
		}
			
		public bool ProgressUserAchievement (Habbo user, string achievementGroup, int progressAmount)
		{
			if (Achievements.ContainsKey (achievementGroup)) {
				Achievement achievement = Achievements [achievementGroup];

				UserAchievement userAchievement = user.Info.Achievements.Single (x => x.Achievement.GroupName == achievementGroup);

				if (userAchievement == null) {
					userAchievement = new UserAchievement () {
						Achievement = achievement,
						Level = achievement.DefaultLevel()
					};

					user.Info.Achievements.Add (userAchievement);
				}

				userAchievement.Progress += progressAmount;
					
				// If is new Level
				if (userAchievement.CanIncreaseLevel()) {
					userAchievement.IncreaseLevel ();

					// Give Reward Points
					user.Info.AchievementPoints += userAchievement.Level.RewardPoints;
					user.Info.Duckets += userAchievement.Level.RewardPixels;

					session.Router.GetComposer<ActivityPointsMessageComposer> ().Compose (user, user.Info.Duckets, user.Info.Diamonds);

					user.RemoveBadge (userAchievement.Achievement.GroupName + (userAchievement.Level.Level - 1));

					// Give new Badge
					user.GiveBadge (achievementGroup + userAchievement.Level.Level);


					// Send Unlocked Composer
					session.Router.GetComposer<UnlockAchievementMessageComposer> ().Compose (session,
						achievement, userAchievement.Level.Level,
						userAchievement.Level.RewardPoints, userAchievement.Level.RewardPixels);

					// Send Score Composer
					session.Router.GetComposer<AchievementPointsMessageComposer> ().Compose (session, user.Info.AchievementPoints);

					// Set Talent
					if (
						Yupi.GetGame ()
                            .GetTalentManager ()
                            .Talents.Values.Any (talent => talent.AchievementGroup == achievementGroup))
						Yupi.GetGame ()
                            .GetTalentManager ()
                            .CompleteUserTalent (session,
							Yupi.GetGame ().GetTalentManager ().GetTalentData (achievementGroup));
				}

				session.Router.GetComposer<AchievementProgressMessageComposer> ().Compose (session, achievement, userAchievement);
				session.Router.GetComposer<UpdateUserDataMessageComposer> ().Compose (session, user);

				return true;
			}

			return false;
		}
			
		public Achievement GetAchievement (string achievementGroup) {
            return Achievements[achievementGroup];
		}
	}
}