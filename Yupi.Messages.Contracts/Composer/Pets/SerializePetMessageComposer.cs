using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SerializePetMessageComposer : AbstractComposer<PetEntity>
    {
        public override void Compose(ISender room, PetEntity pet)
        {
            // Do nothing by default.
        }
    }
}