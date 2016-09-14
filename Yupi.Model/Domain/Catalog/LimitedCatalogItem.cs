using System;

namespace Yupi.Model.Domain
{
    public class LimitedCatalogItem : CatalogItem
    {
        public virtual int LimitedStack { get; protected set; }

        public virtual int LimitedSold { get; protected set; }

        public override bool CanPurchase(Yupi.Model.Domain.Components.UserWallet wallet, int amount = 1)
        {
            return base.CanPurchase(wallet, amount) && LimitedSold < LimitedStack;
        }

        public override void Purchase(Yupi.Model.Domain.Components.UserWallet wallet, int amount = 1)
        {
            base.Purchase(wallet, amount);
            LimitedSold++;
        }
    }
}