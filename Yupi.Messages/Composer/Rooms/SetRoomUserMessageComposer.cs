using System;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class SetRoomUserMessageComposer : AbstractComposer<RoomUser>
	{
	public override void Compose (Yupi.Protocol.ISender room, RoomUser user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				user.Serialize(message, false);
				room.Send (message);
			}
		}	
	}
}

