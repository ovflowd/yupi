namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class CatalogOfferMessageComposer : AbstractComposer<CatalogItem>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, CatalogItem item)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}