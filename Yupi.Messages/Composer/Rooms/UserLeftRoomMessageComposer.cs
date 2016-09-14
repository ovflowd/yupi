using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class UserLeftRoomMessageComposer : Yupi.Messages.Contracts.UserLeftRoomMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int virtualId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				// TODO VirtualId TO STRING?!
				message.AppendString(virtualId.ToString());
				session.Send (message);
			}
		}
	}
}

