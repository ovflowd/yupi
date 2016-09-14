using System;

using Yupi.Messages.Notification;
using Yupi.Model.Domain;

namespace Yupi.Messages.Support
{
	public class ModerationToolSendRoomAlertMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (!session.Info.HasPermission("fuse_alert"))
				return;

			// TODO Unused
			request.GetInteger();

			string message = request.GetString();

			Room room = session.Room;

			session.Room.EachUser (
				(roomSession) => {
					roomSession.Router.GetComposer<SuperNotificationMessageComposer> ()
						.Compose (roomSession, "", message, "", "", "admin", 3);
				}
			);

		}
	}
}

