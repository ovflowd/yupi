using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class PetBreedResultMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int petId, int randomValue)
        {
            // Do nothing by default.
        }
    }
}