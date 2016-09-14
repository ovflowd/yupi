namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class FindMoreFriendsSuccessMessageComposer : AbstractComposer<bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool success)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}