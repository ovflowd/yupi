using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class SerializePetMessageComposer : AbstractComposer<PetEntity>
    {
        public override void Compose(Yupi.Protocol.ISender room, PetEntity pet)
        {
            // Do nothing by default.
        }
    }
}