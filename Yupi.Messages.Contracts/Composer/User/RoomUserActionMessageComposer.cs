using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomUserActionMessageComposer : AbstractComposer<int, UserAction>
    {
        public override void Compose(ISender room, int virtualId, UserAction action)
        {
            // Do nothing by default.
        }
    }
}