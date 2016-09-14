using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RemovePetFromInventoryComposer : AbstractComposer<uint>
    {
        public override void Compose(ISender session, uint petId)
        {
            // Do nothing by default.
        }
    }
}