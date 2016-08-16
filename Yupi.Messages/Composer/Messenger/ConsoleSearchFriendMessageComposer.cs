using System;

using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Util;

namespace Yupi.Messages.Messenger
{
	public class ConsoleSearchFriendMessageComposer : Yupi.Messages.Contracts.ConsoleSearchFriendMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, List<UserInfo> foundFriends, List<UserInfo> foundUsers)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(foundFriends.Count);

				foreach (UserInfo user in foundFriends) {
					//user.Searialize (message);
					throw new NotImplementedException ();
				}
				
				message.AppendInteger(foundUsers.Count);

				foreach (UserInfo user in foundUsers) {
					//user.Searialize (message);
					throw new NotImplementedException ();
				}
				
				session.Send (message);
			}
		}

		private void Searialize(ServerMessage reply, UserInfo user)
		{
			reply.AppendInteger(user.Id);
			reply.AppendString(user.UserName);
			reply.AppendString(user.Motto);
			//reply.AppendBool(Yupi.GetGame().GetClientManager().GetClientByUserId(UserId) != null);
			throw new NotImplementedException ();
			reply.AppendBool(false);
			reply.AppendString(string.Empty);
			reply.AppendInteger(0);
			reply.AppendString(user.Look);
			reply.AppendString(user.LastOnline.ToUnix().ToString());
		}
	}
}

