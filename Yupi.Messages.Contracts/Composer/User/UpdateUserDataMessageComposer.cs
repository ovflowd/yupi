using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateUserDataMessageComposer : AbstractComposer<UserInfo, int>
    {
        public override void Compose(ISender room, UserInfo habbo, int roomUserId = -1)
        {
            // Do nothing by default.
        }
    }
}