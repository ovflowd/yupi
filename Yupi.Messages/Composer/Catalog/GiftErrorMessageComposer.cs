using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class GiftErrorMessageComposer : Contracts.GiftErrorMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, string username)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(username);
                session.Send(message);
            }
        }
    }
}