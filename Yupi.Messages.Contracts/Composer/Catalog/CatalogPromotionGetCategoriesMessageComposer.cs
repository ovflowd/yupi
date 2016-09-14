using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CatalogPromotionGetCategoriesMessageComposer :
        AbstractComposer<IList<PromotionNavigatorCategory>>
    {
        public override void Compose(ISender session, IList<PromotionNavigatorCategory> promotionCategories)
        {
            // Do nothing by default.
        }
    }
}