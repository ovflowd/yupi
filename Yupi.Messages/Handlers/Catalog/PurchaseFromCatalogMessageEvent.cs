using System;
using Yupi.Messages.Notification;

namespace Yupi.Messages.Catalog
{
	public class PurchaseFromCatalogMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Maximum items

			uint pageId = message.GetUInt32();
			uint itemId = message.GetUInt32();
			string extraData = message.GetString();
			uint priceAmount = message.GetUInt32();

			Yupi.GetGame()
				.GetCatalogManager()
				.HandlePurchase(session, pageId, itemId, extraData, priceAmount, false, string.Empty, string.Empty, 0, 0,
					0, false, 0u);
		}
	}
}

