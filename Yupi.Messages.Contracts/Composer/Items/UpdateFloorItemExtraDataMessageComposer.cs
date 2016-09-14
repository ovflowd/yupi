using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateFloorItemExtraDataMessageComposer : AbstractComposer<FloorItem>
    {
        public override void Compose(ISender room, FloorItem item)
        {
            // Do nothing by default.
        }
    }
}