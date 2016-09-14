namespace Yupi.Messages.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GetCatalogIndexMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<CatalogPage> CatalogRepository;

        #endregion Fields

        #region Constructors

        public GetCatalogIndexMessageEvent()
        {
            CatalogRepository = DependencyFactory.Resolve<IRepository<CatalogPage>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            List<CatalogPage> pages = CatalogRepository
                .FilterBy(x => x.Parent == null && x.MinRank <= session.Info.Rank)
                .OrderBy(x => x.OrderNum).ToList();

            // TODO Type?!
            string type = message.GetString().ToUpper();

            router.GetComposer<CatalogueOfferConfigMessageComposer>().Compose(session);
            router.GetComposer<CatalogueIndexMessageComposer>().Compose(session, pages, type, session.Info.Rank);
        }

        #endregion Methods
    }
}