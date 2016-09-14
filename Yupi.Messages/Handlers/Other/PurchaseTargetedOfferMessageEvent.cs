using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;



namespace Yupi.Messages.Other
{
	public class PurchaseTargetedOfferMessageEvent : AbstractHandler
	{
		private IRepository<TargetedOffer> OfferRepository;
		private CatalogController CatalogController;

		public PurchaseTargetedOfferMessageEvent ()
		{
			OfferRepository = DependencyFactory.Resolve<IRepository<TargetedOffer>> ();
			CatalogController = DependencyFactory.Resolve<CatalogController> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int offerId = request.GetInteger();
			int quantity = request.GetInteger();

			TargetedOffer offer = OfferRepository.FindBy(offerId);

			if (offer == null)
				return;

			CatalogController.Purchase(session, offer, string.Empty, quantity);
		}
	}
}

