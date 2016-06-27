using System;

namespace Yupi.Messages.Landing
{
	public class LandingLoadWidgetMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			string text = request.GetString();

			router.GetComposer<LandingWidgetMessageComposer> ().Compose (session, text);
		}
	}
}

