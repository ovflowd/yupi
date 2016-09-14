namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;

    public class TargetedOffer : CatalogItem
    {
        #region Constructors

        public TargetedOffer()
        {
            Products = new List<BaseItem>();
            Icon = string.Empty;
        }

        #endregion Constructors

        #region Properties

        public virtual string Description
        {
            get; set;
        }

        public virtual DateTime ExpiresAt
        {
            get; set;
        }

        public virtual string Icon
        {
            get; set;
        }

        public virtual string Image
        {
            get; set;
        }

        public virtual IList<BaseItem> Products
        {
            get; protected set;
        }

        public virtual int PurchaseLimit
        {
            get; set;
        }

        public virtual string Title
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override bool CanPurchase(Yupi.Model.Domain.Components.UserWallet wallet, int amount = 1)
        {
            // TODO Implement PurchaseLimit
            return base.CanPurchase(wallet, amount);
        }

        #endregion Methods
    }
}