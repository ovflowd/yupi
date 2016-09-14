using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupRoomMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(ISender session, int roomId, int groupId)
        {
            // Do nothing by default.
        }
    }
}