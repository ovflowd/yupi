using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UserClubRightsMessageComposer : AbstractComposer<bool, int, bool>
    {
        public override void Compose(ISender session, bool hasVIP, int rank, bool isAmbadassor = false)
        {
            // Do nothing by default.
        }
    }
}