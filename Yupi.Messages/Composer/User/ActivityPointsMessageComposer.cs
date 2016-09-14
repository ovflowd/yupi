using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class ActivityPointsMessageComposer : Contracts.ActivityPointsMessageComposer
    {
        public override void Compose(ISender session, UserWallet wallet)
        {
            // TODO Can we send credits using this composer too?
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(3); // count

                message.AppendInteger(0); // type
                message.AppendInteger(wallet.Duckets); // value
                message.AppendInteger(5);
                message.AppendInteger(wallet.Diamonds);
                message.AppendInteger(105);
                message.AppendInteger(wallet.Diamonds);
                session.Send(message);
            }
        }
    }
}