using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class FriendRequestsMessageComposer : AbstractComposer<IList<FriendRequest>>
    {
        public override void Compose(ISender session, IList<FriendRequest> requests)
        {
            // Do nothing by default.
        }
    }
}