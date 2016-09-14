using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class PetBreedMessageComposer : AbstractComposer<uint, PetEntity, PetEntity>
    {
        public override void Compose(ISender session, uint furniId, PetEntity pet1, PetEntity pet2)
        {
            // Do nothing by default.
        }
    }
}