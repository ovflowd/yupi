using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateAvatarAspectMessageComposer : AbstractComposer<UserInfo>
    {
        public override void Compose(ISender session, UserInfo habbo)
        {
            // Do nothing by default.
        }
    }
}