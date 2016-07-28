using System;
using Yupi.Messages.Notification;

namespace Yupi.Messages.Catalog
{
	public class PurchaseFromCatalogMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Magic constant !
			if (session.GetHabbo().GetInventoryComponent().TotalItems >= 2799)
			{
				// TODO Should we really send a OK message on failure?
				router.GetComposer<PurchaseOKMessageComposer> ().Compose (session, 0, string.Empty, 0);
				router.GetComposer<SuperNotificationMessageComposer> ().
				Compose (session, 
					"${generic.notice}", 
					"You've exceeded the maximum furnis inventory. You can not buy more until you get rid of some furnis."
				);
				return;
			}

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

