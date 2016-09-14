namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class SubscriptionStatusMessageComposer : AbstractComposer<Subscription>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Subscription subscription)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}