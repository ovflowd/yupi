using System;
using Yupi.Emulator.Game.Users;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class GiveRoomRightsMessageComposer : AbstractComposer<uint, Habbo>
	{
		public override void Compose (Yupi.Protocol.ISender session, uint roomId, Habbo habbo)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(roomId);
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.UserName);
				session.Send (message);
			}
		}
	}
}

