using System;

namespace Yupi.Messages.Catalog
{
	public class PurchaseFromCatalogAsGiftMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			uint pageId = message.GetUInt32();
			uint itemId = message.GetUInt32();
			string extraData = message.GetString();
			string giftUser = message.GetString();
			string giftMessage = message.GetString();
			int giftSpriteId = message.GetInteger();
			int giftLazo = message.GetInteger();
			int giftColor = message.GetInteger();
			bool showSender = message.GetBool(); 

			Yupi.GetGame()
				.GetCatalogManager()
				.HandlePurchase(session, pageId, itemId, extraData, 1, true, giftUser, giftMessage, giftSpriteId,
					giftLazo, giftColor, showSender, 0u);
		}
	}
}

