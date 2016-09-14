using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomUserIdleMessageComposer : AbstractComposer<int, bool>
    {
        public override void Compose(ISender room, int entityId, bool isAsleep)
        {
            // Do nothing by default.
        }
    }
}