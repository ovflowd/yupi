namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class CatalogueIndexMessageComposer : AbstractComposer<IList<CatalogPage>, string, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<CatalogPage> sortedPages, string type,
            int rank)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}