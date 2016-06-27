using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class DanceStatusMessageComposer : AbstractComposer<uint, uint>
	{
		// TODO Create enum for Dances
		
		public override void Compose (Yupi.Protocol.ISender room, uint entityId, uint danceId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (entityId);
				message.AppendInteger(danceId);
				room.Send (message);
			}
		}
	}
}

