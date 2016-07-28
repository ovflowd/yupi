using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class HomeRoomMessageComposer : AbstractComposer<uint>
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint roomId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (roomId);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

