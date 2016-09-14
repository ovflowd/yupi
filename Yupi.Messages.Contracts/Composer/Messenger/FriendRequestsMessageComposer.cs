namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class FriendRequestsMessageComposer : AbstractComposer<IList<FriendRequest>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<FriendRequest> requests)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}