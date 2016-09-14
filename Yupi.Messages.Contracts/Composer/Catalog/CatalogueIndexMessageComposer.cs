using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CatalogueIndexMessageComposer : AbstractComposer<IList<CatalogPage>, string, int>
    {
        public override void Compose(ISender session, IList<CatalogPage> sortedPages, string type, int rank)
        {
            // Do nothing by default.
        }
    }
}