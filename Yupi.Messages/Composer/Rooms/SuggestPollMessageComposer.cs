using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class SuggestPollMessageComposer : Contracts.SuggestPollMessageComposer
    {
        public override void Compose(ISender session, Poll poll)
        {
            using (var message = Pool.GetMessageBuffer(Id))
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