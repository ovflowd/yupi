using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomMuteStatusMessageComposer : AbstractComposer<bool>
    {
        public override void Compose(ISender session, bool isMuted)
        {
            // Do nothing by default.
        }
    }
}