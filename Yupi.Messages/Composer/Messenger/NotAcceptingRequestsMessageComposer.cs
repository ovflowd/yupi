using Yupi.Protocol;

namespace Yupi.Messages.Messenger
{
    public class NotAcceptingRequestsMessageComposer : Contracts.NotAcceptingRequestsMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(39);
                message.AppendInteger(3);
                session.Send(message); // TODO Hardcoded
            }
        }
    }
}