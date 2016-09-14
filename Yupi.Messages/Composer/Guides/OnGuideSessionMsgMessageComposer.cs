using Yupi.Protocol;

namespace Yupi.Messages.Guides
{
    // TODO Rename
    public class OnGuideSessionMsgMessageComposer : Contracts.OnGuideSessionMsgMessageComposer
    {
        public override void Compose(ISender session, string content, int userId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(content);
                message.AppendInteger(userId);
                session.Send(message);
            }
        }
    }
}