using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class HomeRoomMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int roomId)
        {
            // Do nothing by default.
        }
    }
}