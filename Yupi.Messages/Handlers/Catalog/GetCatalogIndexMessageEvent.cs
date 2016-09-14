using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class GetCatalogIndexMessageEvent : AbstractHandler
    {
        private readonly IRepository<CatalogPage> CatalogRepository;

        public GetCatalogIndexMessageEvent()
        {
            CatalogRepository = DependencyFactory.Resolve<IRepository<CatalogPage>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var pages = CatalogRepository
                .FilterBy(x => (x.Parent == null) && (x.MinRank <= session.Info.Rank))
                .OrderBy(x => x.OrderNum).ToList();

            // TODO Type?!
            var type = message.GetString().ToUpper();

            router.GetComposer<CatalogueOfferConfigMessageComposer>().Compose(session);
            router.GetComposer<CatalogueIndexMessageComposer>().Compose(session, pages, type, session.Info.Rank);
        }
    }
}