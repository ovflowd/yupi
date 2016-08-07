using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class RoomUserActionMessageComposer : Yupi.Messages.Contracts.RoomUserActionMessageComposer
	{
		// TODO unknown param?!
		public override void Compose ( Yupi.Protocol.ISender room, int virtualId, int unknown = 7)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(virtualId);
				message.AppendInteger(unknown);
				room.Send (message);
			}
		}
	}
}

