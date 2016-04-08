using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
	public class SecretKeyMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session)
		{
			// TODO Public networks???

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString("Crypto disabled");
				message.AppendBool(false);
				session.Send (message);
			}
		}
	}
}

