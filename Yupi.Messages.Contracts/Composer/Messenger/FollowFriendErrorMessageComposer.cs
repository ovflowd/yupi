namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class FollowFriendErrorMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int status)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}