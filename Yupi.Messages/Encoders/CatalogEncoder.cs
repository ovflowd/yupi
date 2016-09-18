#region Header

// ---------------------------------------------------------------------------------
// <copyright file="CatalogEncoder.cs" company="https://github.com/sant0ro/Yupi">
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

namespace Yupi.Messages.Encoders
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public static class CatalogEncoder
    {
        #region Methods

        public static void Append(this ServerMessage message, CatalogPage page, int rank, CultureInfo lang)
        {
            message.AppendBool(page.Visible);
            message.AppendInteger(page.Icon);
            message.AppendInteger(page.Id);
            message.AppendString(page.Layout.Name);
            message.AppendString(page.Caption.Get(lang));
            message.AppendInteger(page.Offers.Count);

            foreach (CatalogOffer item in page.Offers)
            {
                message.AppendInteger(item.Id);
            }

            IOrderedEnumerable<CatalogPage> sortedSubPages =
                page.Children.Where(x => x.MinRank <= rank).OrderBy(x => x.OrderNum);

            message.AppendInteger(sortedSubPages.Count());

            foreach(CatalogPage child in sortedSubPages) {
                message.Append(child, rank, lang);
            }
        }

        public static void Append(this ServerMessage message, CatalogOffer offer)
        {
            message.AppendInteger(offer.Id);
            message.AppendString(offer.Name);
            message.AppendBool(offer.IsRentable);
            message.AppendInteger(offer.CostCredits);
            message.AppendInteger(offer.CostActivityPoints);
            message.AppendInteger((int)offer.ActivityPointsType);
            message.AppendBool(offer.AllowGift);

            message.AppendInteger (offer.Products.Count);

            foreach (CatalogProduct product in offer.Products) {
                Append(message, product);
            }

            message.AppendInteger ((int)offer.ClubLevel);
            message.AppendBool (offer.IsVisible);
        }

        public static void Append(this ServerMessage message, CatalogProduct product)
        {
            message.AppendString (product.Item.Type.DisplayName);

            if (product.Item.Type == ItemType.Bot)
            {
                message.AppendString (product.Item.Visualization);
            }
            else
            {
                message.AppendInteger(product.Item.Id);
                message.AppendString (product.Item.Visualization);
                message.AppendInteger(product.Amount);

                LimitedCatalogProduct limitedProduct = product as LimitedCatalogProduct;

                message.AppendBool(limitedProduct != null);

                if (limitedProduct != null)
                {
                    message.AppendInteger(limitedProduct.LimitedSeriesSize);
                    message.AppendInteger(limitedProduct.LimitedItemsLeft);
                }
            }
        }

        #endregion Methods
    }
}