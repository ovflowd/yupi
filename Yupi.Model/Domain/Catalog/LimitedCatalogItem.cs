﻿// ---------------------------------------------------------------------------------
// <copyright file="LimitedCatalogItem.cs" company="https://github.com/sant0ro/Yupi">
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

    public class LimitedCatalogItem : CatalogOffer
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