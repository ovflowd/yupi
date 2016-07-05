using System;

namespace Yupi.Messages.Catalog
{
	public class CatalogueOfferConfigMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<CatalogueOfferConfigMessageComposer> ().Compose (session);
		}
	}
}

