using System;







namespace Yupi.Messages.Pets
{
	public class PlacePetMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(session, true)) ||
				!room.CheckRights(session, true))
				return;

			uint petId = request.GetUInt32();

			Pet pet = session.GetHabbo().GetInventoryComponent().GetPet(petId);

			if (pet == null || pet.PlacedInRoom)
				return;

			int x = request.GetInteger();
			int y = request.GetInteger();

			if (!room.GetGameMap().CanWalk(x, y, false))
				return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
				queryReactor.RunFastQuery($"UPDATE pets_data SET room_id = {room.RoomId}, x = {x}, y = {y} WHERE id = {petId}");

			pet.PlacedInRoom = true;
			pet.RoomId = room.RoomId;

			room.GetRoomUserManager()
				.DeployBot(
					new RoomBot(pet.PetId, Convert.ToUInt32(pet.OwnerId), pet.RoomId, AiType.Pet, "freeroam", pet.Name,
						string.Empty, pet.Look, x, y, 0.0, 4, null, null, string.Empty, 0, string.Empty), pet);

			session.GetHabbo().GetInventoryComponent().MovePetToRoom(pet.PetId);

			if (pet.DbState != DatabaseUpdateState.NeedsInsert)
				pet.DbState = DatabaseUpdateState.NeedsUpdate;

			using (IQueryAdapter queryreactor = Yupi.GetDatabaseManager().GetQueryReactor())
				room.GetRoomUserManager().SavePets(queryreactor);

			session.SendMessage(session.GetHabbo().GetInventoryComponent().SerializePetInventory());
		}
	}
}

