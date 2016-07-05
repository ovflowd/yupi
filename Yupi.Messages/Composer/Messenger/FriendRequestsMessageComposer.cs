using System;
using System.Collections.Generic;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
	public class FriendRequestsMessageComposer : AbstractComposer<Dictionary<uint, MessengerRequest>>
	{
		public override void Compose (Yupi.Protocol.ISender session, Dictionary<uint, MessengerRequest> requests)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(requests.Count);
				message.AppendInteger(requests.Count); // TODO why the same value twice?

				foreach (MessengerRequest current in requests)
					current.Serialize(message);
				session.Send (message);
			}
		}
	}
}

