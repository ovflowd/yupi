using System;
using Yupi.Messages.Notification;
using Yupi.Controller;
using Yupi.Model;


namespace Yupi.Messages.Support
{
    public class ModerationToolSendUserAlertMessageEvent : AbstractHandler
    {
        private ClientManager ClientManager;

        public ModerationToolSendUserAlertMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_alert"))
                return;

            int userId = request.GetInteger();
            string message = request.GetString();

            var target = ClientManager.GetByUserId(userId);

            // TODO Log alert

            if (target != null)
            {
                target.Router.GetComposer<AlertNotificationMessageComposer>().Compose(target, message);
            }
        }
    }
}