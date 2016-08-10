using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Model.Domain.Components;
using System;

namespace Yupi.Model.Domain
{
	public class CatalogItem
	{
		public virtual int Id { get; protected set; }
		public virtual bool AllowGift { get; set; }
		public virtual bool ClubOnly { get; protected set; }
		public virtual int CreditsCost { get; protected set; }
		public virtual int DiamondsCost { get; protected set; }
		public virtual int DucketsCost { get; protected set; }
		public virtual bool HaveOffer { get; protected set; }
		public virtual string Name { get; protected set; }
		public virtual string Badge { get; protected set; }

		public virtual CatalogPage PageId { get; set; }
		public virtual IDictionary<BaseItem, int> BaseItems { get; protected set; }

		public CatalogItem ()
		{
			BaseItems = new Dictionary<BaseItem, int> ();
		}

		public virtual bool CanPurchase(UserWallet wallet, int amount = 1) {
			return (this.CreditsCost*amount <= wallet.Credits
				&& this.DucketsCost*amount <= wallet.Duckets
				&& this.DiamondsCost*amount <= wallet.Diamonds);
		}

		public virtual void Purchase(UserWallet wallet, int amount = 1) {
			if (!CanPurchase (wallet, amount)) {
				throw new InvalidOperationException ();
			}

			wallet.Credits -= this.CreditsCost * amount;
			wallet.Duckets -= this.DucketsCost * amount;
			wallet.Diamonds -= this.DiamondsCost * amount;
		}
	}
}