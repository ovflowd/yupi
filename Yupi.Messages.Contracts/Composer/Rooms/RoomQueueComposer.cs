using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomQueueComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int position)
        {
            // Do nothing by default.
        }
    }
}