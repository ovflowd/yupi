using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupAreYouSureMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int userId)
        {
            // Do nothing by default.
        }
    }
}