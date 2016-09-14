using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class CreditsBalanceMessageComposer : Contracts.CreditsBalanceMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int credits)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(credits.ToString());
                session.Send(message);
            }
        }
    }
}