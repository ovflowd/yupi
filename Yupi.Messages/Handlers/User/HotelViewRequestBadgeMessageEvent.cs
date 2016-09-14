using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Model.Repository;

namespace Yupi.Messages.User
{
    public class HotelViewRequestBadgeMessageEvent : AbstractHandler
    {
        private HotelLandingManager HotelView;
        private IRepository<UserInfo> UserRepository;

        public HotelViewRequestBadgeMessageEvent()
        {
            HotelView = DependencyFactory.Resolve<HotelLandingManager>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string name = message.GetString();

            if (!HotelView.HotelViewBadges.ContainsKey(name))
                return;

            string badge = HotelView.HotelViewBadges[name];
            session.Info.Badges.GiveBadge(badge);
            UserRepository.Save(session.Info);
        }
    }
}