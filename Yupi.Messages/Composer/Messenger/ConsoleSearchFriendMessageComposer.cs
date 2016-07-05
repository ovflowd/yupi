using System;

using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
	public class ConsoleSearchFriendMessageComposer : AbstractComposer<List<SearchResult>, List<SearchResult>>
	{
		public override void Compose (Yupi.Protocol.ISender session, List<SearchResult> foundFriends, List<SearchResult> foundUsers)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(foundFriends.Count);

				foreach (SearchResult user in foundFriends)
					user.Searialize(message);
				
				message.AppendInteger(foundUsers.Count);

				foreach (SearchResult user in foundUsers)
					user.Searialize(message);
				
				session.Send (message);
			}
		}
	}
}

