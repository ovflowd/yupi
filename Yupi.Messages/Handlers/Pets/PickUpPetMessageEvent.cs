using System;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Pets.Enums;

namespace Yupi.Messages.Pets
{
	public class PickUpPetMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (session?.GetHabbo() == null || session.GetHabbo().GetInventoryComponent() == null)
				return;

			if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(session, true)))
				return;

			uint petId = request.GetUInt32();

			RoomUser pet = room.GetRoomUserManager().GetPet(petId);

			if (pet == null)
				return;

			if (pet.RidingHorse)
			{
				RoomUser roomUserByVirtualId = room.GetRoomUserManager().GetRoomUserByVirtualId(Convert.ToInt32(pet.HorseId));

				if (roomUserByVirtualId != null)
				{
					roomUserByVirtualId.RidingHorse = false;
					roomUserByVirtualId.ApplyEffect(-1);
					roomUserByVirtualId.MoveTo(new Point(roomUserByVirtualId.X + 1, roomUserByVirtualId.Y + 1));
				}
			}

			if (pet.PetData.DbState != DatabaseUpdateState.NeedsInsert)
				pet.PetData.DbState = DatabaseUpdateState.NeedsUpdate;

			pet.PetData.RoomId = 0;

			session.GetHabbo().GetInventoryComponent().AddPet(pet.PetData);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
				room.GetRoomUserManager().SavePets(queryReactor);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
				queryReactor.RunFastQuery($"UPDATE pets_data SET room_id = 0, x = 0, y = 0 WHERE id = {petId}");

			room.GetRoomUserManager().RemoveBot(pet.VirtualId, false);

			session.SendMessage(session.GetHabbo().GetInventoryComponent().SerializePetInventory());
		}
	}
}

