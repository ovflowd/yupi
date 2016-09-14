using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class PetBreedResultMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(ISender session, int petId, int randomValue)
        {
            // Do nothing by default.
        }
    }
}