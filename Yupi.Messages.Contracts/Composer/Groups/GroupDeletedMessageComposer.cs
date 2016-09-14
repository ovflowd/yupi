using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupDeletedMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender room, int groupId)
        {
            // Do nothing by default.
        }
    }
}