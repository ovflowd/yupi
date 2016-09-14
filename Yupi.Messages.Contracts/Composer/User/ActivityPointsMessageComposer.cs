using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ActivityPointsMessageComposer : AbstractComposer<UserWallet>
    {
        public override void Compose(ISender session, UserWallet wallet)
        {
            // Do nothing by default.
        }
    }
}