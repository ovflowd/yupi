using System;
using Yupi.Net;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class GiveRespectsMessageComposer : AbstractComposer
	{
		public void Compose(Yupi.Messages.Rooms room, int user, int respect) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(user);
				message.AppendInteger(respect);
				room.SendMessage (message);
			}
		}
	}
}

