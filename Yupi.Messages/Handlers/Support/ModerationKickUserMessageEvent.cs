using Yupi.Controller;
using Yupi.Messages.Notification;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationKickUserMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;
        private readonly RoomManager RoomManager;

        public ModerationKickUserMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_kick"))
                return;

            var userId = request.GetInteger();
            var message = request.GetString();

            var target = ClientManager.GetByUserId(userId);

            // TODO Log

            if ((target != null) && (target.Info.Rank < session.Info.Rank))
            {
                RoomManager.RemoveUser(target);
                target.Router.GetComposer<AlertNotificationMessageComposer>().Compose(target, message);
            }
        }
    }
}