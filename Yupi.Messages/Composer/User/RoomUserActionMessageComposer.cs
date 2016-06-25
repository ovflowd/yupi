using System;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.User
{
	public class RoomUserActionMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Protocol.ISender room, int virtualId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(virtualId);
				message.AppendInteger(7); // Magic constant!
				room.SendMessage (message);
			}
		}
	}
}

