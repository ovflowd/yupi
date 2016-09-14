using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class RelationshipsGetMessageEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public RelationshipsGetMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var userId = message.GetInteger();

            var user = UserRepository.FindBy(userId);

            if (user == null)
                return;

            router.GetComposer<RelationshipMessageComposer>().Compose(session, user);
        }
    }
}