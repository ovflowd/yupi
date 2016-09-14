using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class SendPerkAllowancesMessageComposer : AbstractComposer<UserInfo, bool>
    {
        public override void Compose(Yupi.Protocol.ISender session, UserInfo info, bool enableBetaCamera)
        {
            // Do nothing by default.
        }
    }
}