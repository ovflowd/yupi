using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomQueueComposer : AbstractComposer<int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int position)
        {
            // Do nothing by default.
        }
    }
}