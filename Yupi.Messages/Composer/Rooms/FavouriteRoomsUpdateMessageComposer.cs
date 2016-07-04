using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class FavouriteRoomsUpdateMessageComposer : AbstractComposer<uint, bool>
	{
		public override void Compose (Yupi.Protocol.ISender session, uint roomId, bool isAdded)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(roomId);
				message.AppendBool(isAdded);
				session.Send (message);
			}
		}
	}
}

