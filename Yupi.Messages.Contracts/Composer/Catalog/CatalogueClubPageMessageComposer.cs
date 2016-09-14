using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CatalogueClubPageMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int windowId)
        {
            // Do nothing by default.
        }
    }
}