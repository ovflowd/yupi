using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
	public class CheckPetNameMessageComposer : AbstractComposer
	{
		public void Compose(GameClient session, int status, string name) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(status);
				message.AppendString(name);
				session.Send (message);
			}
		}
	}
}

