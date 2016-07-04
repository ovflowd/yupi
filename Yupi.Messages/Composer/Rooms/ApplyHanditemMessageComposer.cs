using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class ApplyHanditemMessageComposer : AbstractComposer<int, int>
	{
		// TODO Really timer and not itemId?
		public override void Compose (Yupi.Protocol.ISender session, int virtualId, int timer)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(virtualId);
				message.AppendInteger(timer);
				session.Send (message);
			}
		}
	}
}

