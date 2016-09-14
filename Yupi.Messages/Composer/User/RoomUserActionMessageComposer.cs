using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
	public class RoomUserActionMessageComposer : Yupi.Messages.Contracts.RoomUserActionMessageComposer
	{
		// TODO unknown param?!
		public override void Compose ( Yupi.Protocol.ISender room, int virtualId, UserAction action)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(virtualId);
				message.AppendInteger(action.Value);
				room.Send (message);
			}
		}
	}
}

