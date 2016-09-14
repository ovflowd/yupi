namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class TradeAcceptMessageComposer : AbstractComposer<uint, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint userId, bool accepted)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}