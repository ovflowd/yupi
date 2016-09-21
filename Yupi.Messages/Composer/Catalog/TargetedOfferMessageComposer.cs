// ---------------------------------------------------------------------------------
// <copyright file="TargetedOfferMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Catalog
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class TargetedOfferMessageComposer : Yupi.Messages.Contracts.TargetedOfferMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, TargetedOffer offer)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int)offer.StateCode);
                message.AppendInteger(offer.Id);
                message.AppendString(offer.Id.ToString());
                message.AppendString(string.Empty); // unknown / unused
                message.AppendInteger(offer.CostCredits);
                message.AppendInteger(offer.CostActivityPoints);
                message.AppendInteger((int)offer.ActivityPointsType);
                message.AppendInteger(offer.PurchaseLimit);

                int timeLeft = 0;

                if (offer.ExpiresAt.HasValue)
                {
                    timeLeft = (int)(offer.ExpiresAt.Value - DateTime.Now).TotalSeconds;
                }

                message.AppendInteger(timeLeft);
                message.AppendString(offer.Name);
                message.AppendString(offer.Description);
                message.AppendString(offer.Image);
                message.AppendString(offer.Icon);

                // These are not necessarily catalog items. Can be anything (declared in productdata.xml)
                message.AppendInteger(offer.Products.Count);

                foreach (CatalogProduct product in offer.Products)
                {
                    message.AppendString(product.Item.Classname); // Taken from productdata.xml
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}