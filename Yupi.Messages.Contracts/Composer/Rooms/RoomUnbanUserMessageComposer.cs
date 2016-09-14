using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomUnbanUserMessageComposer : AbstractComposer<uint, uint>
    {
        public override void Compose(ISender session, uint roomId, uint userId)
        {
            // Do nothing by default.
        }
    }
}