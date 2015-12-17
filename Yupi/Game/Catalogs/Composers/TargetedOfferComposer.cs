using Yupi.Game.Catalogs.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Catalogs.Composers
{
    internal class TargetedOfferComposer
    {
        internal static void GenerateMessage(ServerMessage message, TargetedOffer offer)
        {
            message.Init(LibraryParser.OutgoingRequest("TargetedOfferMessageComposer"));
            message.AppendInteger(1);
            message.AppendInteger(offer.Id);
            message.AppendString(offer.Identifier);
            message.AppendString(offer.Identifier);
            message.AppendInteger(offer.CostCredits);

            if (offer.CostDiamonds > 0)
            {
                message.AppendInteger(offer.CostDiamonds);
                message.AppendInteger(105);
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

            foreach (string product in offer.Products)
            {
                message.AppendString(product);
                message.SaveArray();
            }

            message.EndArray();
        }
    }
}