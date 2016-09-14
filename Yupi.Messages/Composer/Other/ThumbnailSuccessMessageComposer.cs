using Yupi.Protocol;

namespace Yupi.Messages.Other
{
    public class ThumbnailSuccessMessageComposer : Contracts.ThumbnailSuccessMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true);
                message.AppendBool(false); // TODO Hardcoded
                session.Send(message);
            }
        }
    }
}