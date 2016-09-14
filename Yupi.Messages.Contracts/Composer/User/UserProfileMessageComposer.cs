using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class UserProfileMessageComposer : AbstractComposer<UserInfo, UserInfo>
    {
        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo, UserInfo requester)
        {
            // Do nothing by default.
        }
    }
}