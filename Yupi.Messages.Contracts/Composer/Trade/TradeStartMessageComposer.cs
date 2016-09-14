namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class TradeStartMessageComposer : AbstractComposer<uint, uint>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint firstUserId, uint secondUserId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}