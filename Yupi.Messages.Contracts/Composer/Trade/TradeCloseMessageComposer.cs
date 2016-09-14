namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class TradeCloseMessageComposer : AbstractComposer<uint>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint closedById)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}