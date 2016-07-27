using System;


namespace Yupi.Messages.Pets
{
	public class PetGetInformationMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo() == null || session.GetHabbo().CurrentRoom == null)
				return;

			uint petId = request.GetUInt32();

			// TODO Refactor! Pet != User
			RoomUser pet = session.GetHabbo().CurrentRoom.GetRoomUserManager().GetPet(petId);

			if (pet?.PetData == null)
				return;

			router.GetComposer<PetInfoMessageComposer> ().Compose (session, pet);
		}	
	}
}

