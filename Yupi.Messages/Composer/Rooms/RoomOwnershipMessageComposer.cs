using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Rooms
{
	public class RoomOwnershipMessageComposer : Yupi.Messages.Contracts.RoomOwnershipMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomData room, UserInfo user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(room.Id);
				message.AppendBool(room.HasOwnerRights(user));
				session.Send (message);
			}
		}
	}
}

