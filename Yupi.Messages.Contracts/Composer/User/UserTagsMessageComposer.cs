using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UserTagsMessageComposer : AbstractComposer<UserInfo>
    {
        public override void Compose(ISender session, UserInfo info)
        {
            // Do nothing by default.
        }
    }
}