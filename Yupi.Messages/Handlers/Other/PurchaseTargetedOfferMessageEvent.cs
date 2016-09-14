using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class PurchaseTargetedOfferMessageEvent : AbstractHandler
    {
        private readonly CatalogController CatalogController;
        private readonly IRepository<TargetedOffer> OfferRepository;

        public PurchaseTargetedOfferMessageEvent()
        {
            OfferRepository = DependencyFactory.Resolve<IRepository<TargetedOffer>>();
            CatalogController = DependencyFactory.Resolve<CatalogController>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var offerId = request.GetInteger();
            var quantity = request.GetInteger();

            var offer = OfferRepository.FindBy(offerId);

            if (offer == null)
                return;

            CatalogController.Purchase(session, offer, string.Empty, quantity);
        }
    }
}