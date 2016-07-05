using System;


namespace Yupi.Messages.Pets
{
	public class GetPetTrainerPanelMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint petId = request.GetUInt32();

			Yupi.Messages.Rooms currentRoom = session.GetHabbo().CurrentRoom;

			if (currentRoom == null)
				return;

			Pet petData;

			if ((petData = currentRoom.GetRoomUserManager().GetPet(petId).PetData) == null)
				return;

			router.GetComposer<PetTrainerPanelMessageComposer> ().Compose (session, petId);
		}
	}
}

