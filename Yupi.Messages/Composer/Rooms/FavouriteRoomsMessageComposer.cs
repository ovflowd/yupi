using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class FavouriteRoomsMessageComposer : AbstractComposer<List<uint>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, List<uint> rooms)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(30); // TODO Hardcoded value
				message.AppendInteger(rooms.Count);

				foreach (uint i in rooms)
					message.AppendInteger(i);
				session.Send (message);
			}
		}
	}
}

