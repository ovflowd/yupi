using System;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.User
{
	public class RoomUserActionMessageComposer : AbstractComposer
	{
		public override void Compose (Room room, int virtualId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(virtualId);
				message.AppendInteger(7); // Magic constant!
				room.SendMessage (message);
			}
		}
	}
}

