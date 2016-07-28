using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class OnCreateRoomInfoMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomData data)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(data.Id);
				message.AppendString(data.Name);
				session.Send (message);
			}
		}
	}
}

