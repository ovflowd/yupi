using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class HotelViewRequestBadgeMessageEvent : AbstractHandler
    {
        private readonly HotelLandingManager HotelView;
        private readonly IRepository<UserInfo> UserRepository;

        public HotelViewRequestBadgeMessageEvent()
        {
            HotelView = DependencyFactory.Resolve<HotelLandingManager>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var name = message.GetString();

            if (!HotelView.HotelViewBadges.ContainsKey(name))
                return;

            var badge = HotelView.HotelViewBadges[name];
            session.Info.Badges.GiveBadge(badge);
            UserRepository.Save(session.Info);
        }
    }
}