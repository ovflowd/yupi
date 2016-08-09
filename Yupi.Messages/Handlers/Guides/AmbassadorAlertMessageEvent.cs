using System;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Messages.Contracts;
using Yupi.Protocol;


namespace Yupi.Messages.Guides
{
	public class AmbassadorAlertMessageEvent : AbstractHandler
	{
		private ClientManager ClientManager;

		public AmbassadorAlertMessageEvent ()
		{
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission ("send_ambassador_alert"))
				return;

			int userId = message.GetInteger();

			ISession<Habbo> user = ClientManager.GetByUserId (userId);

			user.Router.GetComposer<SuperNotificationMessageComposer> ()
				.Compose (user, "${notification.ambassador.alert.warning.title}",
					"${notification.ambassador.alert.warning.message}");
		}
	}
}

