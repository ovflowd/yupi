using System;


namespace Yupi.Messages.Catalog
{
	public class GetCatalogOfferMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			uint num = message.GetUInt32();

			CatalogItem catalogItem = Yupi.GetGame().GetCatalogManager().GetItemFromOffer(num);

			if (catalogItem == null || CatalogManager.LastSentOffer == num)
				return;

			CatalogManager.LastSentOffer = num;

			router.GetComposer<CatalogOfferMessageComposer> ().Compose (session, catalogItem);
		}
	}
}

