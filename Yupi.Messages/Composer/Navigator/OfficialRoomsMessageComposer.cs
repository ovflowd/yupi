using System;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
	public class OfficialRoomsMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomData roomData)
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

