using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateRoomItemMessageComposer : AbstractComposer<FloorItem>
    {
        public override void Compose(ISender session, FloorItem item)
        {
            // Do nothing by default.
        }
    }
}