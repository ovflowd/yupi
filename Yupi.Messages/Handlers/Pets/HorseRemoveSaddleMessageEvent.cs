using System;


using Yupi.Messages.Rooms;

namespace Yupi.Messages.Pets
{
	public class HorseRemoveSaddleMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(session, true)))
				return;

			uint petId = request.GetUInt32();

			RoomUser pet = room.GetRoomUserManager().GetPet(petId);

			if (pet?.PetData == null || pet.PetData.OwnerId != session.GetHabbo().Id)
				return;

			pet.PetData.HaveSaddle = false;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.RunFastQuery(
					$"UPDATE pets_data SET have_saddle = '0' WHERE id = {pet.PetData.PetId}");

				queryReactor.RunFastQuery(
					$"INSERT INTO items_rooms (user_id, item_name) VALUES ({session.GetHabbo().Id}, 'horse_saddle1');");
			}

			session.GetHabbo().GetInventoryComponent().UpdateItems(true);

			router.GetComposer<SetRoomUserMessageComposer> ().Compose (room, pet);
			router.GetComposer<SerializePetMessageComposer> ().Compose (room, pet);
			*/
			throw new NotImplementedException ();
		}
	}
}