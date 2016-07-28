using System;

using System.Data;

using Yupi.Protocol.Buffers;
using System.Linq;
using Yupi.Model.Domain;


namespace Yupi.Messages.Rooms
{
	public class LoadRoomRightsListMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomData room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (room.Id);
				message.AppendInteger (room.Rights.Count);

				foreach (Habbo habboForId in room.Rights) {
					message.AppendInteger (habboForId.Id);
					message.AppendString (habboForId.UserName);
				}

				session.Send (message);
			}
		}
	}
}

