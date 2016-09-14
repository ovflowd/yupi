using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class RemovePetFromInventoryComposer : AbstractComposer<uint>
    {
        public override void Compose(Yupi.Protocol.ISender session, uint petId)
        {
            // Do nothing by default.
        }
    }
}