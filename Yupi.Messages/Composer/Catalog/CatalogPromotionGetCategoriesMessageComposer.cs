using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;


namespace Yupi.Messages.Catalog
{
    // TODO Rename  (This is Navigator); this has nothing to do with Catalog!
    public class CatalogPromotionGetCategoriesMessageComposer :
        Yupi.Messages.Contracts.CatalogPromotionGetCategoriesMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session,
            IList<PromotionNavigatorCategory> promotionCategories)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(promotionCategories.Count);

                foreach (PromotionNavigatorCategory category in promotionCategories)
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