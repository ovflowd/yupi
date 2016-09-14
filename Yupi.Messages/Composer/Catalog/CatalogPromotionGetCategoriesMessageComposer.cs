namespace Yupi.Messages.Catalog
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    // TODO Rename  (This is Navigator); this has nothing to do with Catalog!
    public class CatalogPromotionGetCategoriesMessageComposer : Yupi.Messages.Contracts.CatalogPromotionGetCategoriesMessageComposer
    {
        #region Methods

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

        #endregion Methods
    }
}