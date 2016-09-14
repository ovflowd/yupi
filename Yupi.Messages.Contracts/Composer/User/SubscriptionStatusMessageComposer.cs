using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SubscriptionStatusMessageComposer : AbstractComposer<Subscription>
    {
        public override void Compose(ISender session, Subscription subscription)
        {
            // Do nothing by default.
        }
    }
}