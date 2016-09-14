using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class PickUpWallItemMessageComposer : AbstractComposer<WallItem, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, WallItem item, int pickerId)
        {
            // Do nothing by default.
        }
    }
}