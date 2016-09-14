using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class GroupDeletedMessageComposer : Yupi.Messages.Contracts.GroupDeletedMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender room, int groupId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (groupId);
				room.Send (message);
			}
		}
	}
}

