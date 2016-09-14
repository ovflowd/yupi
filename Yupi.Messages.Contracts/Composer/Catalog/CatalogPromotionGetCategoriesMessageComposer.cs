using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class CatalogPromotionGetCategoriesMessageComposer : AbstractComposer<IList<PromotionNavigatorCategory>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<PromotionNavigatorCategory> promotionCategories)
		{
		 // Do nothing by default.
		}
	}
}
