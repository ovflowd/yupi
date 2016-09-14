namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ModerationToolUserChatlogMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolUserChatlogMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_chatlogs"))
                return;

            int userId = message.GetInteger();

            UserInfo info = UserRepository.FindBy(userId);

            if (info != null)
            {
                router.GetComposer<ModerationToolUserChatlogMessageComposer>().Compose(session, info);
            }
        }

        #endregion Methods
    }
}