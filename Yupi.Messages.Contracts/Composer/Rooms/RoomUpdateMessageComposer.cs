using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomUpdateMessageComposer : AbstractComposer<int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int roomId)
        {
            // Do nothing by default.
        }
    }
}