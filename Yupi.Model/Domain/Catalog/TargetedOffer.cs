// ---------------------------------------------------------------------------------
// <copyright file="TargetedOffer.cs" company="https://github.com/sant0ro/Yupi">
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

    public class TargetedOffer : CatalogOffer
    {
        #region Enumerations

        public enum TrackingStateCode
        {
            Default = 0,
            WantsToBuy = 1, // Not sure about this
            Rejected = 2,
            Unknown = 3,
            Minimize = 4,
            Maximize1 = 5,
            Maximize2 = 6
        }

        #endregion Enumerations

        #region Properties

        public virtual string Description
        {
            get; set;
        }

        public virtual DateTime? ExpiresAt
        {
            get; set;
        }

        /// <summary>
        /// Icon for minimized view
        /// </summary>
        /// <value>The relative url to the icon.</value>
        public virtual string Icon
        {
            get; set;
        }

        public virtual string Image
        {
            get; set;
        }

        public virtual int PurchaseLimit
        {
            get; set;
        }

        public virtual TrackingStateCode StateCode
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public TargetedOffer()
        {
            Icon = string.Empty;
        }

        #endregion Constructors

        #region Methods

        protected override PurchaseStatus CanPurchase(UserInfo user, int amount = 1)
        {
            // TODO Implement PurchaseLimit
            return base.CanPurchase(user, amount);
        }

        #endregion Methods
    }
}