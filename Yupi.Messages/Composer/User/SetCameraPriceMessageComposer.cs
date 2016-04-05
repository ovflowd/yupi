using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class SetCameraPriceMessageComposer : AbstractComposer
	{
		public void Compose(GameClient session, int credits, int seasonalCurrency) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (credits);
				message.AppendInteger (seasonalCurrency);
				session.Send (message);
			}
		}
	}
}

