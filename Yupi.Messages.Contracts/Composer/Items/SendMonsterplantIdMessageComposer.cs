using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SendMonsterplantIdMessageComposer : AbstractComposer<uint>
    {
        public override void Compose(ISender session, uint entityId)
        {
            // Do nothing by default.
        }
    }
}