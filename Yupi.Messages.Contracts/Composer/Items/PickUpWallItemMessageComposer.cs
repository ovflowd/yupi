using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class PickUpWallItemMessageComposer : AbstractComposer<WallItem, int>
    {
        public override void Compose(ISender session, WallItem item, int pickerId)
        {
            // Do nothing by default.
        }
    }
}