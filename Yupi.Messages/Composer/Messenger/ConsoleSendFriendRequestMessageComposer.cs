using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Messenger
{
    public class ConsoleSendFriendRequestMessageComposer :
        Yupi.Messages.Contracts.ConsoleSendFriendRequestMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, FriendRequest request)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(request.From.Id);
                message.AppendString(request.From.Name);
                message.AppendString(request.From.Look);
                session.Send(message);
            }
        }
    }
}