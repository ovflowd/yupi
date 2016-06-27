using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomEnterErrorMessageComposer : AbstractComposer<RoomEnterErrorMessageComposer.Error>
	{
		public enum Error {
			ROOM_FULL = 1
		}

		public override void Compose (Yupi.Protocol.ISender session, Error error)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (error);
				session.Send (message);
			}
		}
	}
}

