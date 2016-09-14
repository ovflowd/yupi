using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class FollowFriendErrorMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int status)
        {
            // Do nothing by default.
        }
    }
}