using System;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Messages.Pets
{
	public class PetGetInformationMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
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

