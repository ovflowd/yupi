using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
	public class ConsoleSendFriendRequestMessageComposer : AbstractComposer<MessengerRequest>
	{
		public override void Compose (Yupi.Protocol.ISender session, MessengerRequest request)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				request.Serialize(message);
				session.Send (message);
			}
		}
	}
}

