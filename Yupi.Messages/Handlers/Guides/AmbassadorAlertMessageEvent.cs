using Yupi.Controller;
using Yupi.Messages.Contracts;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    public class AmbassadorAlertMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;

        public AmbassadorAlertMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (!session.Info.HasPermission("send_ambassador_alert"))
                return;

            var userId = message.GetInteger();

            var user = ClientManager.GetByUserId(userId);

            user.Router.GetComposer<SuperNotificationMessageComposer>()
                .Compose(user, "${notification.ambassador.alert.warning.title}",
                    "${notification.ambassador.alert.warning.message}");
        }
    }
}