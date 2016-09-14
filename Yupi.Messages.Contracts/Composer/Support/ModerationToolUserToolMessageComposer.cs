using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ModerationToolUserToolMessageComposer : AbstractComposer<UserInfo>
    {
        public override void Compose(ISender session, UserInfo user)
        {
            // Do nothing by default.
        }
    }
}