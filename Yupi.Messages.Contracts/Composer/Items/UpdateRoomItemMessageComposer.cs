using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateRoomItemMessageComposer : AbstractComposer<FloorItem>
    {
        public override void Compose(Yupi.Protocol.ISender session, FloorItem item)
        {
            // Do nothing by default.
        }
    }
}