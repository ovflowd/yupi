namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class OnGuideSessionDetachedMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int value)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}