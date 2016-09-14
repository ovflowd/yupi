namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class FloodFilterMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int remainingSeconds)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}