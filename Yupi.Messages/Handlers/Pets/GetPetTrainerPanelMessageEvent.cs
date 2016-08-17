using System;


namespace Yupi.Messages.Pets
{
	public class GetPetTrainerPanelMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int petId = request.GetInteger();

			// TODO Validate

			router.GetComposer<PetTrainerPanelMessageComposer> ().Compose (session, petId);
		}
	}
}

