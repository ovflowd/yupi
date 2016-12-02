// ---------------------------------------------------------------------------------
// <copyright file="AchievementManager.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Net;
    using Yupi.Protocol;

    public class AchievementManager
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public AchievementManager ()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
            ClientManager = DependencyFactory.Resolve<ClientManager> ();
        }

        #endregion Constructors

        #region Methods

        public bool ProgressUserAchievement (UserInfo info, Achievement achievement)
        {
            // TODO Remove progressAmount?
            Habbo session = ClientManager.GetByInfo (info);
            return ProgressUserAchievement (info, achievement, 1, session);
        }

        public bool ProgressUserAchievement (Habbo user, Achievement achievement)
        {
            return ProgressUserAchievement (user.Info, achievement, 1, user);
        }

        public void TryProgressHabboClubAchievements (Habbo session)
        {
            /*
             *  if (!session.Info.Subscription.IsValid())
                return;
             *
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

        public void TryProgressLoginAchievements (Habbo session)
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

        public void TryProgressRegistrationAchievements (Habbo session)
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

        private bool ProgressUserAchievement (UserInfo user, Achievement achievement, int progressAmount, Habbo session)
        {

            UserAchievement userAchievement =
                user.Achievements.FirstOrDefault (x => x.Achievement == achievement);

            if (userAchievement == null) {
                userAchievement = new UserAchievement (achievement);

                user.Achievements.Add (userAchievement);
            }

            userAchievement.Progress += progressAmount;

            // If is new Level
            if (userAchievement.CanIncreaseLevel ()) {
                Badge oldBadge = userAchievement.Badge;

                userAchievement.IncreaseLevel ();

                // Give Reward Points
                user.Wallet.AchievementPoints += userAchievement.Level.RewardPoints;
                user.Wallet.AddActivityPoints (userAchievement.Level.RewardActivityPointsType,
                    userAchievement.Level.RewardActivityPoints);

                user.Badges.RemoveBadge (oldBadge);
                user.Badges.GiveBadge (userAchievement.Badge);

                if (session != null) {
                    session.Router.GetComposer<ActivityPointsMessageComposer> ().Compose (session, user.Wallet);

                    // Send Unlocked Composer
                    session.Router.GetComposer<UnlockAchievementMessageComposer> ().Compose (session,
                        achievement, userAchievement.Level.Level,
                        userAchievement.Level.RewardPoints, userAchievement.Level.RewardActivityPoints);

                    // Send Score Composer
                    session.Router.GetComposer<AchievementPointsMessageComposer> ()
                        .Compose (session, user.Wallet.AchievementPoints);
                }
                // TODO Reimplement talents properly
            }

            UserRepository.Save (user);

            if (session != null) {
                session.Router.GetComposer<AchievementProgressMessageComposer> ().Compose (session, userAchievement);
                session.Router.GetComposer<UpdateUserDataMessageComposer> ().Compose (session, user);
            }

            return true;
        }

        #endregion Methods
    }
}