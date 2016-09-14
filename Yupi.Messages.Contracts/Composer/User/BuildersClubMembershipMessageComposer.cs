using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class BuildersClubMembershipMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(ISender session, int expire, int maxItems)
        {
            // Do nothing by default.
        }
    }
}