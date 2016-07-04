using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
	public class RetrieveSongIDMessageComposer : AbstractComposer
	{
		public void Compose(GameClient session, string name, int songId) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(name);
				message.AppendInteger(songId);
				session.Send (message);
			}
		}
	}
}

