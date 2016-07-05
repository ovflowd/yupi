using System;



namespace Yupi.Messages.Other
{
	public class PurchaseTargetedOfferMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			request.GetUInt32(); // TODO unused
			uint quantity = request.GetUInt32();

			TargetedOffer offer = Yupi.GetGame().GetTargetedOfferManager().CurrentOffer;

			if (offer == null)
				return;

			// TODO Refactor
			if (session.GetHabbo().Credits < offer.CostCredits * quantity)
				return;

			if (session.GetHabbo().Duckets < offer.CostDuckets * quantity)
				return;

			if (session.GetHabbo().Diamonds < offer.CostDiamonds * quantity)
				return;

			foreach (string product in offer.Products)
			{
				Item item = Yupi.GetGame().GetItemManager().GetItemByName(product);

				if (item == null)
					continue;

				Yupi.GetGame()
					.GetCatalogManager()
					.DeliverItems(session, item, quantity, string.Empty, 0, 0, string.Empty);
			}

			session.GetHabbo().Credits -= offer.CostCredits * quantity;
			session.GetHabbo().Duckets -= offer.CostDuckets * quantity;
			session.GetHabbo().Diamonds -= offer.CostDiamonds * quantity;
			session.GetHabbo().UpdateCreditsBalance();
			session.GetHabbo().UpdateSeasonalCurrencyBalance();
			session.GetHabbo().GetInventoryComponent().UpdateItems(false);
		}
	}
}

