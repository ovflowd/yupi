using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;



namespace Yupi.Messages.Other
{
	public class PurchaseTargetedOfferMessageEvent : AbstractHandler
	{
		private Repository<TargetedOffer> OfferRepository;
		private CatalogController CatalogController;

		public PurchaseTargetedOfferMessageEvent ()
		{
			OfferRepository = DependencyFactory.Resolve<Repository<TargetedOffer>> ();
			CatalogController = DependencyFactory.Resolve<CatalogController> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int offerId = request.GetInteger();
			int quantity = request.GetInteger();

			TargetedOffer offer = OfferRepository.FindBy(offerId);

			if (offer == null)
				return;

			CatalogController.Purchase(session.UserData, offer, string.Empty, quantity);
		}
	}
}

