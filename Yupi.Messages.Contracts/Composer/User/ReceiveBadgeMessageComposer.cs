namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class ReceiveBadgeMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string badgeId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}