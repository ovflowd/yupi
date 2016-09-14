using System;

using System.Data;

using Yupi.Protocol.Buffers;
using System.Linq;
using Yupi.Model.Domain;


namespace Yupi.Messages.Rooms
{
	public class LoadRoomRightsListMessageComposer : Yupi.Messages.Contracts.LoadRoomRightsListMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomData room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (room.Id);
				message.AppendInteger (room.Rights.Count);

				foreach (UserInfo habboForId in room.Rights) {
					message.AppendInteger (habboForId.Id);
					message.AppendString (habboForId.Name);
				}

				session.Send (message);
			}
		}
	}
}

