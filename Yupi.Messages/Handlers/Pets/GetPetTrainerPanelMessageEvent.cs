namespace Yupi.Messages.Pets
{
    using System;

    public class GetPetTrainerPanelMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int petId = request.GetInteger();

            // TODO Validate

            router.GetComposer<PetTrainerPanelMessageComposer>().Compose(session, petId);
        }

        #endregion Methods
    }
}