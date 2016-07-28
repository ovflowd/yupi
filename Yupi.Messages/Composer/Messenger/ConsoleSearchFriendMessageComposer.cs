using System;

using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Messenger
{
	public class ConsoleSearchFriendMessageComposer : AbstractComposer<List<UserInfo>, List<UserInfo>>
	{
		public override void Compose ( Yupi.Protocol.ISender session, List<UserInfo> foundFriends, List<UserInfo> foundUsers)
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

		private void Searialize(ServerMessage reply, UserInfo user)
		{
			reply.AppendInteger(user.Id);
			reply.AppendString(user.UserName);
			reply.AppendString(user.Motto);
			reply.AppendBool(Yupi.GetGame().GetClientManager().GetClientByUserId(UserId) != null);
			reply.AppendBool(false);
			reply.AppendString(string.Empty);
			reply.AppendInteger(0);
			reply.AppendString(user.Look);
			reply.AppendString(user.LastOnline); // TODO Must be double unix timestamp!
		}
	}
}

