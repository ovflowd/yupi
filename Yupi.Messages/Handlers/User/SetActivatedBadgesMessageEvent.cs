namespace Yupi.Messages.User
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class SetActivatedBadgesMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public SetActivatedBadgesMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            session.Info.Badges.ResetSlots();

            for (int i = 0; i < 5; i++)
            {
                int slot = message.GetInteger();
                string code = message.GetString();

                Badge badge = session.Info.Badges.GetBadge(code);

                if (badge == default(Badge) || slot < 1 || slot > 5)
                {
                    continue;
                }

                badge.Slot = slot;
            }

            UserRepository.Save(session.Info);

            router.GetComposer<UserBadgesMessageComposer>().Compose(session, session.Info);
        }

        #endregion Methods
    }
}