using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CataloguePageMessageComposer : AbstractComposer<CatalogPage>
    {
        public override void Compose(ISender session, CatalogPage page)
        {
            // Do nothing by default.
        }
    }
}