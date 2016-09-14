using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolGetRoomVisitsMessageEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public ModerationToolGetRoomVisitsMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (session.Info.HasPermission("fuse_mod"))
            {
                var userId = message.GetInteger();

                var info = UserRepository.FindBy(userId);

                if (info != null) router.GetComposer<ModerationToolRoomVisitsMessageComposer>().Compose(session, info);
            }
        }
    }
}