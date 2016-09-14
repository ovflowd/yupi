// ---------------------------------------------------------------------------------
// <copyright file="SSOManager.cs" company="https://github.com/sant0ro/Yupi">
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
    using Yupi.Util;
    using Yupi.Util.Collections;

    public class SSOManager
    {
        #region Fields

        private AchievementManager AchievementManager;
        private ClientManager ClientManager;
        private ModerationTool ModerationTool;
        private BiDictionary<string, UserInfo> Tickets;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public SSOManager()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            Tickets = new BiDictionary<string, UserInfo>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
            ModerationTool = DependencyFactory.Resolve<ModerationTool>();
        }

        #endregion Constructors

        #region Methods

        public string GenerateTicket(int userId)
        {
            UserInfo user = UserRepository.FindBy(userId);
            return GenerateTicket(user);
        }

        public string GenerateTicket(UserInfo info)
        {
            if (info == null)
            {
                return null;
            }

            lock (Tickets)
            {
                Tickets.TryRemoveBySecond(info);

                string ticket = Cryptography.GetUniqueKey(20);

                if (Tickets.TryAdd(ticket, info))
                {
                    return ticket;
                }
                else
                {
                    return null;
                }
            }
        }

        public void TryLogin(Habbo session, string ssoTicket)
        {
            UserInfo user;

            lock (Tickets)
            {
                Tickets.TryGetByFirst(ssoTicket, out user);
                Tickets.TryRemoveByFirst(ssoTicket);
            }

            if (user != null)
            {
                Habbo toDisconnect = ClientManager.GetByInfo(user);

                if (toDisconnect != null)
                {
                    ClientManager.Disconnect(toDisconnect, "Logged in from other location.");
                }

                session.Info = user;

                // TODO Log login attempts (with IP)

                if (user.IsBanned())
                {
                    ClientManager.Disconnect(session, "Banned from Server.");
                }
                else
                {
                    session.Router.GetComposer<UniqueMachineIDMessageComposer>().Compose(session, session.MachineId);

                    session.Router.GetComposer<AuthenticationOKMessageComposer>().Compose(session);

                    session.Router.GetComposer<HomeRoomMessageComposer>()
                        .Compose(session, user.HomeRoom == null ? 0 : user.HomeRoom.Id);

                    session.Router.GetComposer<MinimailCountMessageComposer>()
                        .Compose(session, user.Minimail.Count(x => !x.Seen));

                    session.Router.GetComposer<FavouriteRoomsMessageComposer>().Compose(session, user.FavoriteRooms);

                    session.Router.GetComposer<UserClubRightsMessageComposer>()
                        .Compose(session, user.Subscription.IsValid(),
                            user.Rank);

                    session.Router.GetComposer<EnableNotificationsMessageComposer>().Compose(session);

                    if (user.CanTrade())
                    {
                        session.Router.GetComposer<EnableTradingMessageComposer>().Compose(session);
                    }
                    session.Router.GetComposer<CreditsBalanceMessageComposer>().Compose(session, user.Wallet.Credits);
                    session.Router.GetComposer<ActivityPointsMessageComposer>().Compose(session, user.Wallet);

                    if (user.HasPermission("fuse_modtool"))
                    {
                        session.Router.GetComposer<LoadModerationToolMessageComposer>()
                            .Compose(session, ModerationTool.Tickets,
                                ModerationTool.Templates, ModerationTool.UserMessagePresets,
                                ModerationTool.RoomMessagePresets, user);
                    }

                    session.Router.GetComposer<SendAchievementsRequirementsMessageComposer>()
                        .Compose(session, AchievementManager.Achievements);
                    session.Router.GetComposer<EffectsInventoryMessageComposer>()
                        .Compose(session, user.EffectComponent.Effects);

                    AchievementManager.TryProgressHabboClubAchievements(session);
                    AchievementManager.TryProgressRegistrationAchievements(session);
                    AchievementManager.TryProgressLoginAchievements(session);
                }
            }
            else
            {
                ClientManager.Disconnect(session, "Invalid SSO Ticket");
            }
        }

        #endregion Methods
    }
}