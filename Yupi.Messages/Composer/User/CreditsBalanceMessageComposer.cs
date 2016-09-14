using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class CreditsBalanceMessageComposer : Contracts.CreditsBalanceMessageComposer
    {
        public override void Compose(ISender session, int credits)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(credits.ToString());
                session.Send(message);
            }
        }
    }
}