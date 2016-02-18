using Yupi.Emulator.Game.Catalogs.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Catalogs.Composers
{
    internal class TargetedOfferComposer
    {
        internal static void GenerateMessage(SimpleServerMessageBuffer messageBuffer, TargetedOffer offer)
        {
            messageBuffer.Init(PacketLibraryManager.OutgoingRequest("TargetedOfferMessageComposer"));
            messageBuffer.AppendInteger(1);
            messageBuffer.AppendInteger(offer.Id);
            messageBuffer.AppendString(offer.Identifier);
            messageBuffer.AppendString(offer.Identifier);
            messageBuffer.AppendInteger(offer.CostCredits);

            if (offer.CostDiamonds > 0)
            {
                messageBuffer.AppendInteger(offer.CostDiamonds);
                messageBuffer.AppendInteger(105);
            }
            else
            {
                messageBuffer.AppendInteger(offer.CostDuckets);
                messageBuffer.AppendInteger(0);
            }

            messageBuffer.AppendInteger(offer.PurchaseLimit);

            int timeLeft = offer.ExpirationTime - Yupi.GetUnixTimeStamp();

            messageBuffer.AppendInteger(timeLeft);
            messageBuffer.AppendString(offer.Title);
            messageBuffer.AppendString(offer.Description);
            messageBuffer.AppendString(offer.Image);
            messageBuffer.AppendString(string.Empty);
            messageBuffer.StartArray();

            foreach (string product in offer.Products)
            {
                messageBuffer.AppendString(product);
                messageBuffer.SaveArray();
            }

            messageBuffer.EndArray();
        }
    }
}