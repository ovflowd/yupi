using System;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class SetRoomUserMessageComposer : AbstractComposer<RoomUser, bool>
	{
		public override void Compose (Yupi.Protocol.ISender room, RoomUser user, bool hasPublicPool = false)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				user.Serialize(message, hasPublicPool);
				room.Send (message);
			}
		}	
	}
}

