using System.Collections.Generic;
using System;

namespace Yupi.Model.Domain
{
	public class TargetedOffer : CatalogItem
    {
		public virtual DateTime ExpiresAt { get; protected set; }
		public virtual int PurchaseLimit { get; protected set; }
		public virtual string Title { get; protected set; }
		public virtual string Description { get; protected set; }
		public virtual string Image { get; protected set; }

		public virtual IList<BaseItem> Products { get; protected set; }

		public TargetedOffer ()
		{
			Products = new List<BaseItem> ();
		}

		public override bool CanPurchase (Yupi.Model.Domain.Components.UserWallet wallet, int amount = 1)
		{
			// TODO Implement PurchaseLimit
			return base.CanPurchase (wallet, amount);
		}
    }
}