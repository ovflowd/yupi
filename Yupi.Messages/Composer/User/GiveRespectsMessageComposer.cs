using System;
using Yupi.Net;

using Yupi.Protocol.Buffers;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
	public class GiveRespectsMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose(ISender room, int user, int respect) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(user);
				message.AppendInteger(respect);
				room.Send(message);
			}
		}
	}
}

