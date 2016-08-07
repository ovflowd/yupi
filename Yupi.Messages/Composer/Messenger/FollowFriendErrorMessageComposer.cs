using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
	public class FollowFriendErrorMessageComposer : Yupi.Messages.Contracts.FollowFriendErrorMessageComposer
	{
		// TODO Enum
		public override void Compose ( Yupi.Protocol.ISender session, int status)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (status);
				session.Send (message);
			}
		}
	}
}

