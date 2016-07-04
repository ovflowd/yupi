using System;
using Yupi.Emulator.Game.Users;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.User
{
	public class UserUpdateNameInRoomMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Protocol.ISender room, Habbo habbo, string newName)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (habbo.Id);
				message.AppendInteger (habbo.CurrentRoom.RoomId);
				message.AppendString (newName);
				room.Send (message);
			}
		}
	}
}

