using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class PetBreedMessageComposer : AbstractComposer<uint, PetEntity, PetEntity>
    {
        public override void Compose(Yupi.Protocol.ISender session, uint furniId, PetEntity pet1, PetEntity pet2)
        {
            // Do nothing by default.
        }
    }
}