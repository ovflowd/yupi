using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class UserClubRightsMessageComposer : AbstractComposer<bool, int, bool>
    {
        public override void Compose(Yupi.Protocol.ISender session, bool hasVIP, int rank, bool isAmbadassor = false)
        {
            // Do nothing by default.
        }
    }
}