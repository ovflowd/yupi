using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CatalogPurchaseNotAllowedMessageComposer : AbstractComposer<bool>
    {
        public override void Compose(ISender session, bool isForbidden)
        {
            // Do nothing by default.
        }
    }
}