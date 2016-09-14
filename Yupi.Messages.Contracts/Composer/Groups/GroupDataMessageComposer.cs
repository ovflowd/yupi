using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupDataMessageComposer : AbstractComposer<Group, UserInfo, bool>
    {
        public override void Compose(ISender session, Group group, UserInfo habbo, bool newWindow = false)
        {
            // Do nothing by default.
        }
    }
}