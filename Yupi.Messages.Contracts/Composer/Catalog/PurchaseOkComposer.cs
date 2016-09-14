namespace Yupi.Messages.Contracts
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;

    public abstract class PurchaseOkComposer : AbstractComposer<CatalogItem, IDictionary<BaseItem, int>, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, CatalogItem itemCatalog,
            IDictionary<BaseItem, int> items,
            int clubLevel = 1)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}