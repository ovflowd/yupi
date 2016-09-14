using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
    public class GetPetTrainerPanelMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var petId = request.GetInteger();

            // TODO Validate

            router.GetComposer<PetTrainerPanelMessageComposer>().Compose(session, petId);
        }
    }
}