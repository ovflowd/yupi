using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
    public class SuggestPollMessageComposer : Yupi.Messages.Contracts.SuggestPollMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, Poll poll)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(Id);
                message.AppendString(string.Empty); //?
                message.AppendString(poll.Invitation);
                message.AppendString("Test"); // whats this??
                session.Send(message);
            }
        }
    }
}