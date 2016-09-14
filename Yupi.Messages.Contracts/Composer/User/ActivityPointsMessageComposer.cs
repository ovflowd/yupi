using Yupi.Protocol.Buffers;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Contracts
{
    public abstract class ActivityPointsMessageComposer : AbstractComposer<UserWallet>
    {
        public override void Compose(Yupi.Protocol.ISender session, UserWallet wallet)
        {
            // Do nothing by default.
        }
    }
}