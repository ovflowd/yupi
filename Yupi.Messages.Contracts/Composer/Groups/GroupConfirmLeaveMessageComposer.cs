using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupConfirmLeaveMessageComposer : AbstractComposer<UserInfo, Group, int>
    {
        public override void Compose(ISender session, UserInfo user, Group group, int type)
        {
            // Do nothing by default.
        }
    }
}