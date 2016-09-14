using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class CatalogOfferMessageComposer : AbstractComposer<CatalogItem>
    {
        public override void Compose(Yupi.Protocol.ISender session, CatalogItem item)
        {
            // Do nothing by default.
        }
    }
}