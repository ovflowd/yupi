using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class PickUpFloorItemMessageComposer : AbstractComposer<FloorItem, int>
    {
        public override void Compose(ISender session, FloorItem item, int pickerId)
        {
            // Do nothing by default.
        }
    }
}