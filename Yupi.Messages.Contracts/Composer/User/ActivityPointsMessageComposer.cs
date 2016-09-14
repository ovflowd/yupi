namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public abstract class ActivityPointsMessageComposer : AbstractComposer<UserWallet>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserWallet wallet)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}