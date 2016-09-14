using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RespectPetMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int entityId)
        {
            // Do nothing by default.
        }
    }
}