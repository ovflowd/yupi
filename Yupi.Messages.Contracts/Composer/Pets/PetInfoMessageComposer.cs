using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class PetInfoMessageComposer : AbstractComposer<PetInfo>
    {
        public override void Compose(ISender room, PetInfo pet)
        {
            // Do nothing by default.
        }
    }
}