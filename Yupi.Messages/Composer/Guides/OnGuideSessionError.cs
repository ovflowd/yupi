using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
	public class OnGuideSessionError : AbstractComposerVoid
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session)
		{
			// TODO Hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (0);
				session.Send (message);
			}
		}
	}
}

