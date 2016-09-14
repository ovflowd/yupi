using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class CataloguePageMessageComposer : AbstractComposer<CatalogPage>
    {
        public override void Compose(Yupi.Protocol.ISender session, CatalogPage page)
        {
            // Do nothing by default.
        }
    }
}