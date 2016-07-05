using System;

namespace Yupi.Messages.User
{
	public class HotelViewCountdownMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string time = message.GetString();
			router.GetComposer<HotelViewCountdownMessageComposer> ().Compose (session, time);
		}
	}
}

