using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class FindMoreFriendsSuccessMessageComposer : AbstractComposer<bool>
    {
        public override void Compose(ISender session, bool success)
        {
            // Do nothing by default.
        }
    }
}