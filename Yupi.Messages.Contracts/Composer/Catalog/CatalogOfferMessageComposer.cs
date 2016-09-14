using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CatalogOfferMessageComposer : AbstractComposer<CatalogItem>
    {
        public override void Compose(ISender session, CatalogItem item)
        {
            // Do nothing by default.
        }
    }
}