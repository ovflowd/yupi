namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class TradeUpdateMessageComposer : AbstractComposer<TradeUser, TradeUser>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, TradeUser first, TradeUser second)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}