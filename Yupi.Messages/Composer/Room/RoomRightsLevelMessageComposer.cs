using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Room
{
	public class RoomRightsLevelMessageComposer : AbstractComposer<int>
	{
		// TODO Level should be enum
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, int level)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (level);
				session.Send (message);
			}
		}
	}
}

