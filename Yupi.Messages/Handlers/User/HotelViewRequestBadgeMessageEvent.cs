namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class HotelViewRequestBadgeMessageEvent : AbstractHandler
    {
        #region Fields

        private HotelLandingManager HotelView;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public HotelViewRequestBadgeMessageEvent()
        {
            HotelView = DependencyFactory.Resolve<HotelLandingManager>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}