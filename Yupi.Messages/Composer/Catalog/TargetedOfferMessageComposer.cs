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
                message.AppendInteger(offer.Id); // Something different, not sure what though
                message.AppendInteger(offer.Id);
                message.AppendString(offer.Id.ToString());
                message.AppendString(offer.Id.ToString());
                message.AppendInteger(offer.CreditsCost);

                if (offer.DiamondsCost > 0)
                {
                    message.AppendInteger(offer.DiamondsCost);
                    message.AppendInteger(105); // TODO Hardcoded (activityPointType)
                }
                else
                {
                    message.AppendInteger(offer.DucketsCost);
                    message.AppendInteger(0);
                }

                message.AppendInteger(offer.PurchaseLimit);

                int timeLeft = (int) (offer.ExpiresAt - DateTime.Now).TotalSeconds;

                message.AppendInteger(timeLeft);
                message.AppendString(offer.Title);
                message.AppendString(offer.Description);
                message.AppendString(offer.Image);
                message.AppendString(offer.Icon);

                message.AppendInteger(offer.Products.Count);

                foreach (BaseItem product in offer.Products)
                {
                    message.AppendString(product.Name);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}