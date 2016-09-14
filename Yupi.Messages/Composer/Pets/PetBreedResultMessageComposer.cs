using Yupi.Protocol;

namespace Yupi.Messages.Pets
{
    public class PetBreedResultMessageComposer : Contracts.PetBreedResultMessageComposer
    {
        public override void Compose(ISender session, int petId, int randomValue)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(petId);
                message.AppendInteger(randomValue);
                session.Send(message);
            }
        }
    }
}