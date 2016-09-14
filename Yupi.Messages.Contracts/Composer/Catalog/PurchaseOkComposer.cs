using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class PurchaseOkComposer : AbstractComposer<CatalogItem, IDictionary<BaseItem, int>, int>
    {
        public override void Compose(ISender session, CatalogItem itemCatalog, IDictionary<BaseItem, int> items,
            int clubLevel = 1)
        {
            // Do nothing by default.
        }
    }
}