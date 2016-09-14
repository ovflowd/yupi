namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class PurchaseTargetedOfferMessageEvent : AbstractHandler
    {
        #region Fields

        private CatalogController CatalogController;
        private IRepository<TargetedOffer> OfferRepository;

        #endregion Fields

        #region Constructors

        public PurchaseTargetedOfferMessageEvent()
        {
            OfferRepository = DependencyFactory.Resolve<IRepository<TargetedOffer>>();
            CatalogController = DependencyFactory.Resolve<CatalogController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int offerId = request.GetInteger();
            int quantity = request.GetInteger();

            TargetedOffer offer = OfferRepository.FindBy(offerId);

            if (offer == null)
                return;

            CatalogController.Purchase(session, offer, string.Empty, quantity);
        }

        #endregion Methods
    }
}