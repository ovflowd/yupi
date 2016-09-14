namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class CatalogPromotionGetCategoriesMessageComposer : AbstractComposer<IList<PromotionNavigatorCategory>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session,
            IList<PromotionNavigatorCategory> promotionCategories)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}