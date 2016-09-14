using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RemoveRightsMessageComposer : Yupi.Messages.Contracts.RemoveRightsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint roomId, uint userId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (roomId);
				message.AppendInteger (userId);
				session.Send (message);
			}
		}
	}
}

