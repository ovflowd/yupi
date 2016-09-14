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
namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Yupi.Model.Domain.Components;

    public class CatalogItem
    {
        #region Properties

        public virtual bool AllowGift
        {
            get; set;
        }

        public virtual string Badge
        {
            get; set;
        }

        public virtual IDictionary<BaseItem, int> BaseItems
        {
            get; protected set;
        }

        public virtual bool ClubOnly
        {
            get; set;
        }

        // TODO Rename to CostCredits?
        public virtual int CreditsCost
        {
            get; set;
        }

        public virtual int DiamondsCost
        {
            get; set;
        }

        public virtual int DucketsCost
        {
            get; set;
        }

        public virtual bool HaveOffer
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual string Name
        {
            get; set;
        }

        public virtual CatalogPage PageId
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public CatalogItem()
        {
            BaseItems = new Dictionary<BaseItem, int>();
        }

        #endregion Constructors

        #region Methods

        public virtual bool CanPurchase(UserWallet wallet, int amount = 1)
        {
            return (this.CreditsCost*amount <= wallet.Credits
                    && this.DucketsCost*amount <= wallet.Duckets
                    && this.DiamondsCost*amount <= wallet.Diamonds);
        }

        public virtual void Purchase(UserWallet wallet, int amount = 1)
        {
            if (!CanPurchase(wallet, amount))
            {
                throw new InvalidOperationException();
            }

            wallet.Credits -= this.CreditsCost*amount;
            wallet.Duckets -= this.DucketsCost*amount;
            wallet.Diamonds -= this.DiamondsCost*amount;
        }

        #endregion Methods
    }
}