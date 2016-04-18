using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class FavouriteGroupMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, int userId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (userId);
				session.Send (message);
			}
		}
	}
}

