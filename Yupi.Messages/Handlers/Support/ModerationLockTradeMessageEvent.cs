using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;
using Yupi.Messages.Notification;


namespace Yupi.Messages.Support
{
    public class ModerationLockTradeMessageEvent : AbstractHandler
    {
        private IRepository<UserInfo> UserRepository;
        private ClientManager ClientManager;

        public ModerationLockTradeMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

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
    }
}