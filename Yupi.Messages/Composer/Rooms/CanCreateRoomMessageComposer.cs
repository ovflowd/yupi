using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class CanCreateRoomMessageComposer : Yupi.Messages.Contracts.CanCreateRoomMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, UserInfo user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(user.UsersRooms.Count >= 75 ? 1 : 0); // TODO Enum
				message.AppendInteger(75); // TODO Magic number
				session.Send (message);
			}
		}
	}
}

