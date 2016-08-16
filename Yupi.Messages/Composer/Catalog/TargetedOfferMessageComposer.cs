using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Catalog
{
	public class TargetedOfferMessageComposer : Yupi.Messages.Contracts.TargetedOfferMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, IList<TargetedOffer> offers)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(offers.Count);

				foreach (TargetedOffer offer in offers) {

					message.AppendInteger (offer.Id);
					message.AppendString (offer.Id.ToString());
					message.AppendString (offer.Id.ToString());
					message.AppendInteger (offer.CreditsCost);

					if (offer.DiamondsCost > 0) {
						message.AppendInteger (offer.DiamondsCost);
						message.AppendInteger (105); // TODO Hardcoded
					} else {
						message.AppendInteger (offer.DucketsCost);
						message.AppendInteger (0);
					}

					message.AppendInteger (offer.PurchaseLimit);

					int timeLeft = (int)(offer.ExpiresAt - DateTime.Now).TotalSeconds;

					message.AppendInteger (timeLeft);
					message.AppendString (offer.Title);
					message.AppendString (offer.Description);
					message.AppendString (offer.Image);
					message.AppendString (string.Empty);

					message.AppendInteger (offer.Products.Count);

					foreach (BaseItem product in offer.Products) {
						message.AppendString (product.Name);
					}
				}
				session.Send (message);
			}
		}
	}
}

