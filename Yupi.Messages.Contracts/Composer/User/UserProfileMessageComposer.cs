using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UserProfileMessageComposer : AbstractComposer<UserInfo, UserInfo>
    {
        public override void Compose(ISender session, UserInfo habbo, UserInfo requester)
        {
            // Do nothing by default.
        }
    }
}