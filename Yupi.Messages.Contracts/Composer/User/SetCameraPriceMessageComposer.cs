namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class SetCameraPriceMessageComposer : AbstractComposer<int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int credits, int seasonalCurrency)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}