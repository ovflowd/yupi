namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class GiftWrappingConfigurationMessageComposer : AbstractComposerVoid
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}