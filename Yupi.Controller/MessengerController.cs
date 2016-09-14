using System;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Messages.Contracts;
using Yupi.Protocol;
using Yupi.Net;

namespace Yupi.Controller
{
	public class MessengerController
	{
		private ClientManager ClientManager;

		public MessengerController ()
		{
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
		}

		public void UpdateUser(UserInfo user) {
			foreach (Relationship friend in user.Relationships.Relationships) {
				if (ClientManager.IsOnline (friend.Friend)) {
					Habbo session = ClientManager.GetByInfo (friend.Friend);
					Relationship relationship = session.Info.Relationships.FindByUser (user);

					session.Router.GetComposer<FriendUpdateMessageComposer> ()
						.Compose (session, relationship);
				}
			}
		}
	}
}

