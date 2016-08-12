using System;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Messages.Notification;


namespace Yupi.Messages.Support
{
	public class ModerationToolSendUserCautionMessageEvent : AbstractHandler
	{
		private ClientManager ClientManager;

		public ModerationToolSendUserCautionMessageEvent ()
		{
			ClientManager = DependencyFactory.Resolve<ClientManager>();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission("fuse_alert"))
				return;

			int userId = request.GetInteger();
			string message = request.GetString();

			var target = ClientManager.GetByUserId (userId);

			// TODO Log caution

			if (target != null) {
				target.Router.GetComposer<AlertNotificationMessageComposer> ().Compose(target, message);
			}
		}
	}
}

