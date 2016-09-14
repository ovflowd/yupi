using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class TargetedOfferMessageComposer : AbstractComposer<TargetedOffer>
    {
        public override void Compose(ISender session, TargetedOffer offer)
        {
            // Do nothing by default.
        }
    }
}