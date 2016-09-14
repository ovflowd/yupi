using Yupi.Protocol;

namespace Yupi.Messages.Other
{
    public class InternalLinkMessageComposer : Contracts.InternalLinkMessageComposer
    {
        public override void Compose(ISender session, string link)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(link);
                session.Send(message);
            }
        }
    }
}