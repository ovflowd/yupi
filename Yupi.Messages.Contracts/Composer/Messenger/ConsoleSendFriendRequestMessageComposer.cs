namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class ConsoleSendFriendRequestMessageComposer : AbstractComposer<FriendRequest>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, FriendRequest request)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}