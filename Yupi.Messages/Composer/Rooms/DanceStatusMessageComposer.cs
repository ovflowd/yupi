using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class DanceStatusMessageComposer : Yupi.Messages.Contracts.DanceStatusMessageComposer
	{
		// TODO Create enum for Dances
		
		public override void Compose ( Yupi.Protocol.ISender room, uint entityId, uint danceId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (entityId);
				message.AppendInteger(danceId);
				room.Send (message);
			}
		}
	}
}

