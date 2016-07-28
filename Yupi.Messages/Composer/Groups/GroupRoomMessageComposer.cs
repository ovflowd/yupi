using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class GroupRoomMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose ( Yupi.Protocol.ISender session, int roomId, int groupId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (roomId);
				message.AppendInteger (groupId);
				session.Send (message);
			}
		}
	}
}

