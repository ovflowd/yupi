using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolUserChatlogMessageEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public ModerationToolUserChatlogMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_chatlogs"))
                return;

            var userId = message.GetInteger();

            var info = UserRepository.FindBy(userId);

            if (info != null) router.GetComposer<ModerationToolUserChatlogMessageComposer>().Compose(session, info);
        }
    }
}