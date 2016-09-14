using Yupi.Protocol;

namespace Yupi.Messages.Guides
{
    public class OnGuideSessionErrorComposer : Contracts.OnGuideSessionErrorComposer
    {
        public override void Compose(ISender session)
        {
            // TODO Hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}