using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class GroupDeletedMessageComposer : AbstractComposer
	{
		public override void Compose (Room room, int groupId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (groupId);
				room.Send (message);
			}
		}
	}
}

