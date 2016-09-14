using Yupi.Messages.Notification;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolSendRoomAlertMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_alert"))
                return;

            // TODO Unused
            request.GetInteger();

            var message = request.GetString();

            var room = session.Room;

            session.Room.EachUser(
                roomSession =>
                {
                    roomSession.Router.GetComposer<SuperNotificationMessageComposer>()
                        .Compose(roomSession, "", message, "", "", "admin", 3);
                }
            );
        }
    }
}