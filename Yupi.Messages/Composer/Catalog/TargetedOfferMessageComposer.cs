using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class TargetedOfferMessageComposer : Contracts.TargetedOfferMessageComposer
    {
        public override void Compose(ISender session, TargetedOffer offer)
        {
            using (var message = Pool.GetMessageBuffer(Id))
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

                var timeLeft = (int) (offer.ExpiresAt - DateTime.Now).TotalSeconds;

                message.AppendInteger(timeLeft);
                message.AppendString(offer.Title);
                message.AppendString(offer.Description);
                message.AppendString(offer.Image);
                message.AppendString(offer.Icon);

                message.AppendInteger(offer.Products.Count);

                foreach (var product in offer.Products) message.AppendString(product.Name);

                session.Send(message);
            }
        }
    }
}