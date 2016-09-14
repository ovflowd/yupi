using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class UpdateIgnoreStatusMessageComposer : Yupi.Messages.Contracts.UpdateIgnoreStatusMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, State state, string username)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) state);
                message.AppendString(username);
                session.Send(message);
            }
        }
    }
}