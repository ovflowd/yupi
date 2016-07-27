using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class RoomChatOptionsMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomData data)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(data.ChatType);
				message.AppendInteger(data.ChatBalloon);
				message.AppendInteger(data.ChatSpeed);
				message.AppendInteger(data.ChatMaxDistance);
				message.AppendInteger(data.ChatFloodProtection);
				session.Send (message);
			}
		}
	}
}

