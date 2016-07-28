using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Navigator
{
	public class OfficialRoomsMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomData roomData)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(roomData.Id);
				message.AppendString(roomData.CcTs);
				message.AppendInteger(roomData.Id);
				session.Send (message);
			}
		}
	}
}

