using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
	public class InitCryptoMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				// TODO What about public networks?
				message.AppendString("Yupi");
				message.AppendString("Disabled Crypto");
				session.Send (message);
			}
		}
	}
}

