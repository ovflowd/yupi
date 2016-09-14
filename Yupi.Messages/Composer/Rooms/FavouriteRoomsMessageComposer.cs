using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class FavouriteRoomsMessageComposer : Yupi.Messages.Contracts.FavouriteRoomsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, IList<RoomData> rooms)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(30); // TODO Hardcoded value
				message.AppendInteger(rooms.Count);

				foreach (RoomData room in rooms)
					message.AppendInteger(room.Id);
				session.Send (message);
			}
		}
	}
}

