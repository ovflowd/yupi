namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ModerationToolGetRoomVisitsMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolGetRoomVisitsMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (session.Info.HasPermission("fuse_mod"))
            {
                int userId = message.GetInteger();

                UserInfo info = UserRepository.FindBy(userId);

                if (info != null)
                {
                    router.GetComposer<ModerationToolRoomVisitsMessageComposer>().Compose(session, info);
                }
            }
        }

        #endregion Methods
    }
}