using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class InitialRoomInfoMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, RoomData room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(room.Model.Id);
				message.AppendInteger(room.Id);
				session.Send (message);
			}
		}
	}
}

