using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateAvatarAspectMessageComposer : AbstractComposer<UserInfo>
    {
        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
        {
            // Do nothing by default.
        }
    }
}