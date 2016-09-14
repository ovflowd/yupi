namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.Notification;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ModerationLockTradeMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public ModerationLockTradeMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_lock_trade"))
                return;

            int userId = request.GetInteger();
            string message = request.GetString();
            int hours = request.GetInteger();

            UserInfo user = UserRepository.FindBy(userId);

            user.TradeLocks.Add(new TradeLock()
            {
                ExpiresAt = DateTime.Now.AddHours(hours)
            });

            UserRepository.Save(user);

            var target = ClientManager.GetByInfo(user);
            if (target != null)
            {
                target.Router.GetComposer<AlertNotificationMessageComposer>().Compose(target, message);
            }
        }

        #endregion Methods
    }
}