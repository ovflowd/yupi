using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class GetCatalogClubPageMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var windowId = message.GetInteger();
            router.GetComposer<CatalogueClubPageMessageComposer>().Compose(session, windowId);
        }
    }
}