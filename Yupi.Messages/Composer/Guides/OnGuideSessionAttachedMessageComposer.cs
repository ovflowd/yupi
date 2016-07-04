using System;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.Guides
{
	public class OnGuideSessionAttachedMessageComposer : AbstractComposer
	{
		// TODO Find the meaning of val1 & val2
		public void Compose(GameClient session, bool val1, int userId, string message, int val2) {
			using (ServerMessage response = Pool.GetMessageBuffer (Id)) {
				response.AppendBool(false);
				response.AppendInteger(userId);
				response.AppendString(message);
				response.AppendInteger(30);
				session.Send (response);
			}
		}
	}
}

