namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class CataloguePageMessageComposer : AbstractComposer<CatalogPage>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, CatalogPage page)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}