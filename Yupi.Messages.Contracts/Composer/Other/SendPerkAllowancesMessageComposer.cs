using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SendPerkAllowancesMessageComposer : AbstractComposer<UserInfo, bool>
    {
        public override void Compose(ISender session, UserInfo info, bool enableBetaCamera)
        {
            // Do nothing by default.
        }
    }
}