using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UserIsPlayingFreezeMessageComposer : AbstractComposer<bool>
    {
        public override void Compose(ISender session, bool isPlaying)
        {
            // Do nothing by default.
        }
    }
}