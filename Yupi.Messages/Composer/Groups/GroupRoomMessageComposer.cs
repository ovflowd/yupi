using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class GroupRoomMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, int roomId, int groupId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (roomId);
				message.AppendInteger (groupId);
				session.Send (message);
			}
		}
	}
}

