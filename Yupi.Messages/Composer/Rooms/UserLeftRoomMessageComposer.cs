using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class UserLeftRoomMessageComposer : AbstractComposer<int>
	{
		public override void Compose ( Yupi.Protocol.ISender session, int virtualId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(virtualId);
				session.Send (message);
			}
		}
	}
}

