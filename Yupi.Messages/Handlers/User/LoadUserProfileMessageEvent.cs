using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class LoadUserProfileMessageEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public LoadUserProfileMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var userId = message.GetInteger();
            message.GetBool(); // TODO Unused (never set to false in client?)

            var user = UserRepository.FindBy(userId);

            if (user == null)
                return;

            router.GetComposer<UserProfileMessageComposer>().Compose(session, user, session.Info);
            router.GetComposer<UserBadgesMessageComposer>().Compose(session, user);
        }
    }
}