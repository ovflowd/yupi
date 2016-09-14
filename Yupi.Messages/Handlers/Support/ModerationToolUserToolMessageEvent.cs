namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ModerationToolUserToolMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<SupportTicket> SupportRepository;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolUserToolMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            SupportRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            // TODO Rewrite rights management to prevent usage of strings...
            if (session.Info.HasPermission("fuse_mod"))
            {
                int userId = message.GetInteger();

                UserInfo info = UserRepository.FindBy(userId);
                var tickets = SupportRepository.FilterBy(x => x.Sender == session.Info);

                router.GetComposer<ModerationToolUserToolMessageComposer>().Compose(session, info);
            }
        }

        #endregion Methods
    }
}