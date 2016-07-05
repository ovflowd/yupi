using System;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Core.Io.Logger;
using System.Collections.Generic;

namespace Yupi.Messages.Rooms
{
	public class SetRoomUserMessageComposer : AbstractComposer<List<RoomUser>, bool>
	{
		public override void Compose (Yupi.Protocol.ISender room, List<RoomUser> users, bool hasPublicPool = false)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (users.Count);

				foreach (RoomUser user in users)
				{
						user.Serialize(message, hasPublicPool);
				}

				room.Send (message);
			}
		}	

		public override void Compose (Yupi.Protocol.ISender room, RoomUser user, bool hasPublicPool = false)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (1);
				user.Serialize(message, hasPublicPool);
				room.Send (message);
			}
		}	
	}
}

