using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomUserIdleMessageComposer : AbstractComposer<uint, bool>
	{
		public override void Compose ( Yupi.Protocol.ISender room, uint entityId, bool isAsleep)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (entityId);
				message.AppendBool(isAsleep);
				room.Send (message);
			}
		}
	}
}

