using Yupi.Protocol;

namespace Yupi.Messages.Guides
{
    public class OnGuideSessionDetachedMessageComposer : Contracts.OnGuideSessionDetachedMessageComposer
    {
        // TODO Meaning of value (enum)
        public override void Compose(ISender session, int value)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(value);
                session.Send(message);
            }
        }
    }
}