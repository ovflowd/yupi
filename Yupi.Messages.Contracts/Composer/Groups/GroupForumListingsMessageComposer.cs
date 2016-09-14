using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumListingsMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(ISender session, int selectType, int startIndex)
        {
            // Do nothing by default.
        }
    }
}