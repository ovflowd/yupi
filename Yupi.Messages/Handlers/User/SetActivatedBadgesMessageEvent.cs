using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class SetActivatedBadgesMessageEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public SetActivatedBadgesMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            session.Info.Badges.ResetSlots();

            for (var i = 0; i < 5; i++)
            {
                var slot = message.GetInteger();
                var code = message.GetString();

                var badge = session.Info.Badges.GetBadge(code);

                if ((badge == default(Badge)) || (slot < 1) || (slot > 5)) continue;

                badge.Slot = slot;
            }

            UserRepository.Save(session.Info);

            router.GetComposer<UserBadgesMessageComposer>().Compose(session, session.Info);
        }
    }
}