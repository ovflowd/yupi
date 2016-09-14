namespace Yupi.Messages.User
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    public class LoadUserProfileMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public LoadUserProfileMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int userId = message.GetInteger();
            message.GetBool(); // TODO Unused (never set to false in client?)

            UserInfo user = UserRepository.FindBy(userId);

            if (user == null)
            {
                return;
            }

            router.GetComposer<UserProfileMessageComposer>().Compose(session, user, session.Info);
            router.GetComposer<UserBadgesMessageComposer>().Compose(session, user);
        }

        #endregion Methods
    }
}