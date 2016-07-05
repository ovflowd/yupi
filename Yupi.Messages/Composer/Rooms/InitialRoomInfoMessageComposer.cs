using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class InitialRoomInfoMessageComposer : AbstractComposer<Room>
	{
		public override void Compose (Yupi.Protocol.ISender session, Room room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(room.RoomData.ModelName);
				message.AppendInteger(room.RoomId);
				session.Send (message);
			}
		}
	}
}

