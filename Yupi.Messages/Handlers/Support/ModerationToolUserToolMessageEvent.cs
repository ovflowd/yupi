using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolUserToolMessageEvent : AbstractHandler
    {
        private readonly IRepository<SupportTicket> SupportRepository;
        private readonly IRepository<UserInfo> UserRepository;

        public ModerationToolUserToolMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            SupportRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            // TODO Rewrite rights management to prevent usage of strings...
            if (session.Info.HasPermission("fuse_mod"))
            {
                var userId = message.GetInteger();

                var info = UserRepository.FindBy(userId);
                var tickets = SupportRepository.FilterBy(x => x.Sender == session.Info);


                router.GetComposer<ModerationToolUserToolMessageComposer>().Compose(session, info);
            }
        }
    }
}