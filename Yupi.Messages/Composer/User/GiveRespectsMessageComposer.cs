using System;
using Yupi.Net;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.User
{
	public class GiveRespectsMessageComposer : AbstractComposer
	{
		public void Compose(Room room, int user, int respect) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(user);
				message.AppendInteger(respect);
				room.SendMessage (message);
			}
		}
	}
}

