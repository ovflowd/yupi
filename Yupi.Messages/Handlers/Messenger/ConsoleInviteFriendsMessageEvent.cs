using System;
using System.Collections.Generic;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;


namespace Yupi.Messages.Messenger
{
	public class ConsoleInviteFriendsMessageEvent : AbstractHandler
	{
		private ClientManager ClientManager;

		public ConsoleInviteFriendsMessageEvent ()
		{
			ClientManager = DependencyFactory.Resolve<ClientManager>();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int count = request.GetInteger();

			int[] users = new int[count];

			for (int i = 0; i < count; i++) {
				users[i] = request.GetInteger ();
			}

			string content = request.GetString();

			foreach (int userId in users) {
				Relationship relationship = session.Info.Relationships.FindByUser (userId);
				if (relationship == null) {
					continue;
				}

				var friendSession = ClientManager.GetByInfo (relationship.Friend);
			
				friendSession?.Router.GetComposer<ConsoleInvitationMessageComposer> ().Compose (friendSession, session.Info.Id, content);
			}
		}
	}
}