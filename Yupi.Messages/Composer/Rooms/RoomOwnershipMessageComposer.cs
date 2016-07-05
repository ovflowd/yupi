using System;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Rooms
{
	public class RoomOwnershipMessageComposer : AbstractComposer<Room, GameClient>
	{
		public override void Compose (Yupi.Protocol.ISender session, Room room, GameClient user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(room.RoomId);
				message.AppendBool(room.CheckRights(user, true));
				session.Send (message);
			}
		}
	}
}

