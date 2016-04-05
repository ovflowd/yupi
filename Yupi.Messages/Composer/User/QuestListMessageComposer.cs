using System;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class QuestListMessageComposer : AbstractComposerVoid
	{
		public override void Compose (GameClient session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (0); // TODO What do these values mean?
				message.AppendBool (true);
				session.Send (message);
			}
		}
	}
}

