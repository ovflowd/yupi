using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class RoomForwardMessageComposer : Yupi.Messages.Contracts.RoomForwardMessageComposer
	{
		// TODO Use RoomInfo
		public override void Compose ( Yupi.Protocol.ISender session, int roomId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (roomId);
				session.Send (message);
			}
		}
	}
}

