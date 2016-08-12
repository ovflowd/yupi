using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.Messenger
{
	// TODO Rename?
	public class ConsoleInstantChatMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;
		private ClientManager ClientManager;

		public ConsoleInstantChatMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>>();
			ClientManager = DependencyFactory.Resolve<ClientManager>();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int toId = request.GetInteger();
			string text = request.GetString();

			if (string.IsNullOrWhiteSpace (text))
				return;
			
			Relationship friend = session.UserData.Info.Relationships.FindByUser (toId);

			if (friend != null) {
				MessengerMessage message = new MessengerMessage () {
					From = session.UserData.Info,
					Text = text,
					Timestamp = DateTime.Now
				};

				var friendSession = ClientManager.GetByInfo (friend.Friend);
				friendSession?.Router.GetComposer<ConsoleChatMessageComposer> ().Compose (session, message);

				// TODO Filter
				// TODO Store for offline
				// TODO Store for chatlog
			}

		    session.GetHabbo ().GetMessenger ().SendInstantMessage (toId, text);
		}
	}
}

