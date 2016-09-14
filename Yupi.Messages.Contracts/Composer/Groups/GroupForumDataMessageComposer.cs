using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumDataMessageComposer : AbstractComposer<Group, UserInfo>
    {
        public override void Compose(Yupi.Protocol.ISender session, Group group, UserInfo user)
        {
            // Do nothing by default.
        }
    }
}