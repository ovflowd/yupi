namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class ApplyHanditemMessageComposer : AbstractComposer<int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int virtualId, int itemId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}