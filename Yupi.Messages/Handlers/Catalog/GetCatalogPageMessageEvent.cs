namespace Yupi.Messages.Catalog
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GetCatalogPageMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<CatalogPage> CatalogRepository;

        #endregion Fields

        #region Constructors

        public GetCatalogPageMessageEvent()
        {
            CatalogRepository = DependencyFactory.Resolve<IRepository<CatalogPage>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int pageId = message.GetInteger();

            message.GetInteger(); // TODO unused

            CatalogPage page = CatalogRepository.FindBy(pageId);

            if (page == null || !page.Enabled || !page.Visible || page.MinRank > session.Info.Rank)
                return;

            router.GetComposer<CataloguePageMessageComposer>().Compose(session, page);
        }

        #endregion Methods
    }
}