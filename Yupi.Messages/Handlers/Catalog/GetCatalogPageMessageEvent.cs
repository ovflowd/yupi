using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class GetCatalogPageMessageEvent : AbstractHandler
    {
        private readonly IRepository<CatalogPage> CatalogRepository;

        public GetCatalogPageMessageEvent()
        {
            CatalogRepository = DependencyFactory.Resolve<IRepository<CatalogPage>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var pageId = message.GetInteger();

            message.GetInteger(); // TODO unused

            var page = CatalogRepository.FindBy(pageId);

            if ((page == null) || !page.Enabled || !page.Visible || (page.MinRank > session.Info.Rank))
                return;

            router.GetComposer<CataloguePageMessageComposer>().Compose(session, page);
        }
    }
}