﻿using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.Messenger
{
	// TODO Rename?
	public class ConsoleInstantChatMessageEvent : AbstractHandler
	{
		private IRepository<UserInfo> UserRepository;
		private ClientManager ClientManager;
		private WordfilterManager Wordfilter;

		public ConsoleInstantChatMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
			ClientManager = DependencyFactory.Resolve<ClientManager>();
			Wordfilter = DependencyFactory.Resolve<WordfilterManager>();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int toId = request.GetInteger();
			string text = request.GetString();

			if (string.IsNullOrWhiteSpace (text))
				return;
			
			Relationship friend = session.Info.Relationships.FindByUser (toId);

			if (friend != null) {
				MessengerMessage message = new MessengerMessage () {
					From = session.Info,
					UnfilteredText = text,
					Text = Wordfilter.Filter(text),
					Timestamp = DateTime.Now,
				};
					
				var friendSession = ClientManager.GetByInfo (friend.Friend);
				friendSession?.Router.GetComposer<ConsoleChatMessageComposer> ().Compose (session, message);
				message.Read = friendSession != null;

				// TODO Store for offline
				// TODO Store for chatlog
			}
		}
	}
}
