using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class OnCreateRoomInfoMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomData data)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(data.Id);
				message.AppendString(data.Name);
				session.Send (message);
			}
		}
	}
}

