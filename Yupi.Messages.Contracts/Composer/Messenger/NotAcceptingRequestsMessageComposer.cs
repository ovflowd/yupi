using System;

namespace Yupi.Messages.Contracts
{
    public class NotAcceptingRequestsMessageComposer : AbstractComposerVoid
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
        }
    }
}