using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.GameCenter
{
	public class GameCenterGamesListMessageComposer : AbstractComposerVoid
	{
		// TODO Hardcoded message
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(18);
				message.AppendString("elisa_habbo_stories");
				message.AppendString("000000");
				message.AppendString("ffffff");
				message.AppendString("");
				message.AppendString("");

				session.Send (message);
			}
		}
	}
}

