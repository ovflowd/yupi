using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class UpdateUsernameMessageComposer : Yupi.Messages.Contracts.UpdateUsernameMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, string newName)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); // TODO Magic constant
                message.AppendString(newName);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}