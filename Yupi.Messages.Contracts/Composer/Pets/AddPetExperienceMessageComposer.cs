using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class AddPetExperienceMessageComposer : AbstractComposer<PetEntity, int>
    {
        public override void Compose(ISender session, PetEntity pet, int amount)
        {
            // Do nothing by default.
        }
    }
}