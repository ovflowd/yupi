using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Rooms
{
	public class RoomOwnershipMessageComposer : AbstractComposer<RoomData, UserInfo>
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomData room, UserInfo user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(room.Id);
				message.AppendBool(room.CheckRights(user, true));
				session.Send (message);
			}
		}
	}
}

