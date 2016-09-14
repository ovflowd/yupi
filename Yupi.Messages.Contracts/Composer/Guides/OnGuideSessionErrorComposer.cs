using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OnGuideSessionErrorComposer : AbstractComposerVoid
    {
        public override void Compose(ISender session)
        {
            // Do nothing by default.
        }
    }
}