using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class CatalogueOfferConfigMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            router.GetComposer<CatalogueOfferConfigMessageComposer>().Compose(session);
        }
    }
}