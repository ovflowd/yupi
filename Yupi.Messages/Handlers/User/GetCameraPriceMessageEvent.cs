using System;

namespace Yupi.Messages.User
{
	public class GetCameraPriceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Replace hardcoded values
			router.GetComposer<SetCameraPriceMessageComposer> ().Compose (session, 0, 10);
		}
	}
}

