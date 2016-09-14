using System;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Messages.Notification;

namespace Yupi.Messages.Support
{
    public class ModerationMuteUserMessageEvent : AbstractHandler
    {
        private ClientManager ClientManager;

        public ModerationMuteUserMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mute"))
                return;

            int userId = request.GetInteger();
            string message = request.GetString();

            session.Info.Muted = true;

            var target = ClientManager.GetByUserId(userId);

            // TODO Log mute

            if (target != null)
            {
                target.Router.GetComposer<AlertNotificationMessageComposer>().Compose(target, message);
            }
        }
    }
}