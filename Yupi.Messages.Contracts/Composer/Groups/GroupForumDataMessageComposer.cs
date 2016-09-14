using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumDataMessageComposer : AbstractComposer<Group, UserInfo>
    {
        public override void Compose(ISender session, Group group, UserInfo user)
        {
            // Do nothing by default.
        }
    }
}