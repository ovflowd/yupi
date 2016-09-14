using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    // TODO Rename  (This is Navigator); this has nothing to do with Catalog!
    public class CatalogPromotionGetCategoriesMessageComposer : Contracts.CatalogPromotionGetCategoriesMessageComposer
    {
        public override void Compose(ISender session, IList<PromotionNavigatorCategory> promotionCategories)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(promotionCategories.Count);

                foreach (var category in promotionCategories)
                {
                    message.AppendInteger(category.Id);
                    message.AppendString(category.Caption);
                    message.AppendBool(category.Visible);
                }

                session.Send(message);
            }
        }
    }
}