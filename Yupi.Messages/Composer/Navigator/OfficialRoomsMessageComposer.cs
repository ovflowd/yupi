using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Navigator
{
	public class OfficialRoomsMessageComposer : Yupi.Messages.Contracts.OfficialRoomsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomData roomData)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(roomData.Id);
				message.AppendString(roomData.CCTs);
				message.AppendInteger(roomData.Id);
				session.Send (message);
			}
		}
	}
}

