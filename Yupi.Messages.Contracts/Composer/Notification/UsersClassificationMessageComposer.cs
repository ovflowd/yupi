using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UsersClassificationMessageComposer : AbstractComposer<UserInfo, string>
    {
        public override void Compose(ISender session, UserInfo habbo, string word)
        {
            // Do nothing by default.
        }
    }
}