using Yupi.Controller;
using Yupi.Messages.Notification;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolSendUserCautionMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;

        public ModerationToolSendUserCautionMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_alert"))
                return;

            var userId = request.GetInteger();
            var message = request.GetString();

            var target = ClientManager.GetByUserId(userId);

            // TODO Log caution

            if (target != null) target.Router.GetComposer<AlertNotificationMessageComposer>().Compose(target, message);
        }
    }
}