using System;
using System.Collections.Generic;
using Yupi.Emulator.Game.Users.Messenger.Structs;
using Yupi.Emulator.Game.Users.Messenger;

namespace Yupi.Messages.Messenger
{
	public class ConsoleSearchFriendsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			// TODO Can the messenger ever be null?
			if (session.GetHabbo().GetMessenger() == null) 
				return;

			string query = request.GetString ();

			List<SearchResult> searchResult = SearchResultFactory.GetSearchResult(query);

			List<SearchResult> foundFriends = new List<SearchResult>();
			List<SearchResult> foundUsers = new List<SearchResult>();

			foreach (SearchResult current in searchResult)
			{
				if (session.GetHabbo().GetMessenger().FriendshipExists(current.UserId))
					foundFriends.Add(current);
				else
					foundUsers.Add(current);
			}

			router.GetComposer<ConsoleSearchFriendMessageComposer> ().Compose (session, foundFriends, foundUsers);

		}
	}
}

