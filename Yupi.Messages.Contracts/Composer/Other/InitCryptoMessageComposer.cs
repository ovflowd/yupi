using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class InitCryptoMessageComposer : AbstractComposerVoid
    {
        public override void Compose(ISender session)
        {
            // Do nothing by default.
        }
    }
}