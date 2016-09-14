using System;
using Yupi.Net;

using Yupi.Protocol.Buffers;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
	public class GiveRespectsMessageComposer : Yupi.Messages.Contracts.GiveRespectsMessageComposer
	{
		public override void Compose( Yupi.Protocol.ISender room, int user, int respect) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(user);
				message.AppendInteger(respect);
				room.Send(message);
			}
		}
	}
}

