using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomUserActionMessageComposer : AbstractComposer<int, UserAction>
    {
        public override void Compose(Yupi.Protocol.ISender room, int virtualId, UserAction action)
        {
            // Do nothing by default.
        }
    }
}