using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomRightsLevelMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int level)
        {
            // Do nothing by default.
        }
    }
}