using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class SetCameraPriceMessageComposer : Contracts.SetCameraPriceMessageComposer
    {
        public override void Compose(ISender session, int credits, int seasonalCurrency)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(credits);
                message.AppendInteger(seasonalCurrency);
                session.Send(message);
            }
        }
    }
}