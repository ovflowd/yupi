#region Header

// ---------------------------------------------------------------------------------
// <copyright file="CatalogItem.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------

#endregion Header

namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.Serialization;

    using Yupi.Model.Domain.Components;

    
    public class CatalogOffer
    {
        #region Properties

        [Required]
        public virtual ActivityPointsType ActivityPointsType {
            get; set;
        } = ActivityPointsType.Duckets;

        [Required]
        public virtual bool AllowGift {
            get;
            set;
        } = false;

        public virtual Badge Badge
        {
            get;
            set;
        }

        [Required]
        public virtual ClubLevel ClubLevel
        {
            get;
            set;
        } = ClubLevel.Normal;

        [Required]
        public virtual int CostActivityPoints
        {
            get;
            set;
        }

        [Required]
        public virtual int CostCredits
        {
            get;
            set;
        }

        [Key]
        public virtual int Id
        {
            get;
            protected set;
        }

        [Required]
        public virtual bool IsRentable {
            get;
            set;
        } = false;

        [Required]
        public virtual bool IsVisible {
            get;
            set;
        } = true;

        [Required]
        public virtual TString Name
        {
            get;
            set;
        }

        [ManyToMany]
        public virtual IList<CatalogProduct> Products
        {
            get;
            protected set;
        } = new List<CatalogProduct> ();

        #endregion Properties

        #region Methods

        public virtual PurchaseStatus Purchase(UserInfo user, int amount = 1)
        {
            PurchaseStatus status = CanPurchase(user, amount);

            if (status == PurchaseStatus.Ok) {
                user.Wallet.Credits -= this.CostCredits * amount;
                user.Wallet.SubstractActivityPoints(this.ActivityPointsType, this.CostActivityPoints * amount);
            }

            return status;
        }

        protected virtual PurchaseStatus CanPurchase(UserInfo info, int amount = 1)
        {
            if (this.CostCredits * amount > info.Wallet.Credits
                && this.CostActivityPoints * amount > info.Wallet.GetActivityPoints(this.ActivityPointsType))
            {
                return PurchaseStatus.NotEnoughCredits;
            }
            else if (!info.Subscription.HasLevel(this.ClubLevel))
            {
                return PurchaseStatus.InvalidSubscription;
            }
            else
            {
                foreach (CatalogProduct product in this.Products)
                {
                    PurchaseStatus status = product.CanPurchase(amount);
                    if (status != PurchaseStatus.Ok)
                    {
                        return status;
                    }
                }
            }

            return PurchaseStatus.Ok;
        }

        #endregion Methods
    }
}