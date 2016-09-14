using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ConsoleSendFriendRequestMessageComposer : AbstractComposer<FriendRequest>
    {
        public override void Compose(ISender session, FriendRequest request)
        {
            // Do nothing by default.
        }
    }
}