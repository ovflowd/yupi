namespace Yupi.Messages.User
{
    using System;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class RelationshipsGetMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public RelationshipsGetMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int userId = message.GetInteger();

            UserInfo user = UserRepository.FindBy(userId);

            if (user == null)
                return;

            router.GetComposer<RelationshipMessageComposer>().Compose(session, user);
        }

        #endregion Methods
    }
}