using Yupi.Controller;
using Yupi.Messages.Notification;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationMuteUserMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;

        public ModerationMuteUserMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mute"))
                return;

            var userId = request.GetInteger();
            var message = request.GetString();

            session.Info.Muted = true;

            var target = ClientManager.GetByUserId(userId);

            // TODO Log mute

            if (target != null) target.Router.GetComposer<AlertNotificationMessageComposer>().Compose(target, message);
        }
    }
}