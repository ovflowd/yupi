using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class MatchingPollAnsweredMessageComposer : Contracts.MatchingPollAnsweredMessageComposer
    {
        public override void Compose(ISender session, uint userId, string text)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(userId);
                message.AppendString(text);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}