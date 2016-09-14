namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Messages.Notification;
    using Yupi.Model.Domain;

    public class ModerationToolSendRoomAlertMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_alert"))
                return;

            // TODO Unused
            request.GetInteger();

            string message = request.GetString();

            Room room = session.Room;

            session.Room.EachUser(
                (roomSession) =>
                {
                    roomSession.Router.GetComposer<SuperNotificationMessageComposer>()
                        .Compose(roomSession, "", message, "", "", "admin", 3);
                }
            );
        }

        #endregion Methods
    }
}