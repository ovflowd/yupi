using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupDataMessageComposer : AbstractComposer<Group, UserInfo, bool>
    {
        public override void Compose(Yupi.Protocol.ISender session, Group group, UserInfo habbo, bool newWindow = false)
        {
            // Do nothing by default.
        }
    }
}