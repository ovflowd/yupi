using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class NotifyNewPetLevelMessageComposer : AbstractComposer<PetInfo>
    {
        public override void Compose(ISender session, PetInfo pet)
        {
            // Do nothing by default.
        }
    }
}