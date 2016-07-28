using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomMuteStatusMessageComposer : AbstractComposer<bool>
	{
		public override void Compose ( Yupi.Protocol.ISender session, bool isMuted)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(isMuted);
				session.Send (message);
			}
		}
	}
}

