using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Pets
{
    public class AddPetExperienceMessageComposer : Contracts.AddPetExperienceMessageComposer
    {
        public override void Compose(ISender session, PetEntity pet, int amount)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(pet.Info.Id);
                message.AppendInteger(pet.Id);
                message.AppendInteger(amount);
                session.Send(message);
            }
        }
    }
}