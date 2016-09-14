using System;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
    public abstract class PurchaseOkComposer : AbstractComposer<CatalogItem, IDictionary<BaseItem, int>, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, CatalogItem itemCatalog,
            IDictionary<BaseItem, int> items,
            int clubLevel = 1)
        {
            // Do nothing by default.
        }
    }
}