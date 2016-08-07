using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Catalog
{
	public class TargetedOfferMessageComposer : Yupi.Messages.Contracts.TargetedOfferMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, TargetedOffer offer)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(offer.Id);
				message.AppendString(offer.Identifier);
				message.AppendString(offer.Identifier);
				message.AppendInteger(offer.CostCredits);

				if (offer.CostDiamonds > 0)
				{
					message.AppendInteger(offer.CostDiamonds);
					message.AppendInteger(105); // TODO Hardcoded
				}
				else
				{
					message.AppendInteger(offer.CostDuckets);
					message.AppendInteger(0);
				}

				message.AppendInteger(offer.PurchaseLimit);

				int timeLeft = offer.ExpirationTime - Yupi.GetUnixTimeStamp();

				message.AppendInteger(timeLeft);
				message.AppendString(offer.Title);
				message.AppendString(offer.Description);
				message.AppendString(offer.Image);
				message.AppendString(string.Empty);
				message.StartArray();

				foreach (BaseItem product in offer.Products)
				{
					message.AppendString(product.Name);
					message.SaveArray();
				}

				message.EndArray();

				session.Send (message);
			}
		}
	}
}

