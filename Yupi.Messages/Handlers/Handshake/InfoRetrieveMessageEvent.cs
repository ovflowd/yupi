// ---------------------------------------------------------------------------------
// <copyright file="InfoRetrieveMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Handshake
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;
    using Yupi.Util.Settings;

    public class InfoRetrieveMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<MessengerMessage> MessengerRepository;
        private IRepository<TargetedOffer> OfferRepository;
        private IRepository<PromotionNavigatorCategory> PromotionRepository;

        #endregion Fields

        #region Constructors

        public InfoRetrieveMessageEvent()
        {
            MessengerRepository = DependencyFactory.Resolve<IRepository<MessengerMessage>>();
            OfferRepository = DependencyFactory.Resolve<IRepository<TargetedOffer>>();
            PromotionRepository = DependencyFactory.Resolve<IRepository<PromotionNavigatorCategory>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<UserObjectMessageComposer>().Compose(session, session.Info);

            // TODO Hardcoded (welcome lobby not developed)
            router.GetComposer<NewbieStatusMessageComposer>().Compose(session, NewbieStatus.NORMAL);
            router.GetComposer<BuildersClubMembershipMessageComposer>().Compose(
                session,
                session.Info.BuilderInfo.BuildersExpire,
                session.Info.BuilderInfo.BuildersItemsMax
            );

            router.GetComposer<SendPerkAllowancesMessageComposer>()
                .Compose(session, session.Info, GameSettings.EnableCamera);

            InitMessenger(session, router);

            router.GetComposer<CitizenshipStatusMessageComposer>().Compose(session, "citizenship");
            router.GetComposer<GameCenterGamesListMessageComposer>().Compose(session);
            router.GetComposer<AchievementPointsMessageComposer>()
                .Compose(session, session.Info.Wallet.AchievementPoints);

            // TODO Should these really be send here?
            //router.GetComposer<FigureSetIdsMessageComposer> ().Compose (session);
            router.GetComposer<CatalogPromotionGetCategoriesMessageComposer>()
                .Compose(session, PromotionRepository.All().ToList());

            /*
            // TODO Implement TargetedOffer completely
            TargetedOffer offer = new TargetedOffer() {
                ExpiresAt = DateTime.Now.AddDays(1),
                Title = "Test",
                Description = "Test",
                CreditsCost = 1,
                Image = "test.png",

            };

            router.GetComposer<TargetedOfferMessageComposer> ().Compose (session, offer);
            */
        }

        private void InitMessenger(Yupi.Model.Domain.Habbo session, Yupi.Protocol.IRouter router)
        {
            router.GetComposer<LoadFriendsCategoriesComposer>().Compose(session);

            IRepository<FriendRequest> requests = DependencyFactory.Resolve<IRepository<FriendRequest>> ();

            router.GetComposer<LoadFriendsMessageComposer>().Compose(session, session.Info.Relationships.Relationships);
            router.GetComposer<FriendRequestsMessageComposer>()
                  .Compose(session, requests.FilterBy(x => x.To == session.Info).ToList());

            var messages = MessengerRepository.FilterBy(x => x.To == session.Info && !x.Read);

            foreach (MessengerMessage message in messages)
            {
                router.GetComposer<ConsoleChatMessageComposer>().Compose(session, message);
                message.Read = true;
                MessengerRepository.Save(message);
            }
        }

        #endregion Methods
    }
}