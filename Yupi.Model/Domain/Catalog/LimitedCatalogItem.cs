namespace Yupi.Model.Domain
{
    using System;

    public class LimitedCatalogItem : CatalogItem
    {
        #region Properties

        public virtual int LimitedSold
        {
            get; protected set;
        }

        public virtual int LimitedStack
        {
            get; protected set;
        }

        #endregion Properties

        #region Methods

        public override bool CanPurchase(Yupi.Model.Domain.Components.UserWallet wallet, int amount = 1)
        {
            return base.CanPurchase(wallet, amount) && LimitedSold < LimitedStack;
        }

        public override void Purchase(Yupi.Model.Domain.Components.UserWallet wallet, int amount = 1)
        {
            base.Purchase(wallet, amount);
            LimitedSold++;
        }

        #endregion Methods
    }
}