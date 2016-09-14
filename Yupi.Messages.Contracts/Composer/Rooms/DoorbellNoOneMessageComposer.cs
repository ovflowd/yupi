using System;

namespace Yupi.Messages.Contracts
{
    public abstract class DoorbellNoOneMessageComposer : AbstractComposerVoid
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            // Do nothing by default.
        }
    }
}