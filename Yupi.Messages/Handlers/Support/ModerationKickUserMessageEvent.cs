using System;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Messages.Notification;


namespace Yupi.Messages.Support
{
	public class ModerationKickUserMessageEvent : AbstractHandler
	{
		private ClientManager ClientManager;
		private RoomManager RoomManager;

		public ModerationKickUserMessageEvent ()
		{
			ClientManager = DependencyFactory.Resolve<ClientManager>();
			RoomManager = DependencyFactory.Resolve<RoomManager>();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (!session.Info.HasPermission("fuse_kick"))
				return;

			int userId = request.GetInteger();
			string message = request.GetString();

			var target = ClientManager.GetByUserId (userId);

			// TODO Log

			if (target != null && target.Info.Rank < session.Info.Rank) {
				RoomManager.RemoveUser (target.RoomEntity);
				target.Router.GetComposer<AlertNotificationMessageComposer> ().Compose(target, message);
			}
		}
	}
}

