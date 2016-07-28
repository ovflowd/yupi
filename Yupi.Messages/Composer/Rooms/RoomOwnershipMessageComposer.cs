using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Rooms
{
	public class RoomOwnershipMessageComposer : AbstractComposer<RoomData, Habbo>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, RoomData room, Habbo user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(room.Id);
				message.AppendBool(room.CheckRights(user, true));
				session.Send (message);
			}
		}
	}
}

