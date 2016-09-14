using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupMembersMessageComposer : AbstractComposer<UserInfo, Group>
    {
        public override void Compose(ISender session, UserInfo user, Group group)
        {
            // Do nothing by default.
        }

        public virtual void Compose(ISender session, Group group, uint reqType, UserInfo user, string searchVal = "",
            int page = 0)
        {
            // Do nothing by default.
        }
    }
}