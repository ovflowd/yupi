using System;
using Yupi.Controller;
using Yupi.Messages.Notification;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationLockTradeMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;
        private readonly IRepository<UserInfo> UserRepository;

        public ModerationLockTradeMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_lock_trade"))
                return;

            var userId = request.GetInteger();
            var message = request.GetString();
            var hours = request.GetInteger();

            var user = UserRepository.FindBy(userId);

            user.TradeLocks.Add(new TradeLock
            {
                ExpiresAt = DateTime.Now.AddHours(hours)
            });

            UserRepository.Save(user);

            var target = ClientManager.GetByInfo(user);
            if (target != null) target.Router.GetComposer<AlertNotificationMessageComposer>().Compose(target, message);
        }
    }
}