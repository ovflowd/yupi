using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class UserIsPlayingFreezeMessageComposer : AbstractComposer<bool>
    {
        public override void Compose(Yupi.Protocol.ISender session, bool isPlaying)
        {
            // Do nothing by default.
        }
    }
}