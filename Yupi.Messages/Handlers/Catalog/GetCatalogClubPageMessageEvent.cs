using System;

namespace Yupi.Messages.Catalog
{
	public class GetCatalogClubPageMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int windowId = message.GetInteger();
			router.GetComposer<CatalogueClubPageMessageComposer> ().Compose (session, windowId);
		}
	}
}

