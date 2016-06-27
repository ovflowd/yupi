using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class FloodFilterMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Protocol.ISender session, int remainingSeconds)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (remainingSeconds);
				session.Send (message);
			}
		}
	}
}

