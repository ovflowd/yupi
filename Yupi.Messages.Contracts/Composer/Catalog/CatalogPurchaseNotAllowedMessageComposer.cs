namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class CatalogPurchaseNotAllowedMessageComposer : AbstractComposer<bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool isForbidden)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}