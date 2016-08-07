using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class CanCreateRoomMessageComposer : Yupi.Messages.Contracts.CanCreateRoomMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				// TODO Refactor
				message.AppendInteger(session.GetHabbo().UsersRooms.Count >= 75 ? 1 : 0);
				message.AppendInteger(75);
				session.Send (message);
			}
		}
	}
}

