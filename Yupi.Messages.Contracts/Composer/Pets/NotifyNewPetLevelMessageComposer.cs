using Yupi.Model.Domain;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class NotifyNewPetLevelMessageComposer : AbstractComposer<PetInfo>
    {
        public override void Compose(Yupi.Protocol.ISender session, PetInfo pet)
        {
            // Do nothing by default.
        }
    }
}