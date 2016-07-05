using System;





namespace Yupi.Messages.Items
{
	public class CompostMonsterplantMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_1"));
				return;
			}

			uint moplaId = request.GetUInt32();

			RoomUser pet = room.GetRoomUserManager().GetPet(moplaId);

			if (pet == null || !pet.IsPet || pet.PetData.Type != "pet_monster" || pet.PetData.MoplaBreed == null)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_2"));
				return;
			}

			if (pet.PetData.MoplaBreed.LiveState != MoplaState.Dead)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_3"));
				return;
			}

			Item compostItem = Yupi.GetGame().GetItemManager().GetItemByName("mnstr_compost");

			if (compostItem == null)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_4"));
				return;
			}

			int x = pet.X;
			int y = pet.Y;
			double z = pet.Z;

			room.GetRoomUserManager().RemoveBot(pet.VirtualId, false);

			using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				dbClient.SetQuery(
					"INSERT INTO items_rooms (user_id, room_id, item_name, extra_data, x, y, z) VALUES (@uid, @rid, @bit, '0', @ex, @wai, @zed)");
				dbClient.AddParameter("uid", session.GetHabbo().Id);
				dbClient.AddParameter("rid", room.RoomId);
				dbClient.AddParameter("bit", compostItem.Name);
				dbClient.AddParameter("ex", x);
				dbClient.AddParameter("wai", y);
				dbClient.AddParameter("zed", z);

				uint itemId = (uint) dbClient.InsertQuery();

				RoomItem roomItem = new RoomItem(itemId, room.RoomId, compostItem.Name, "0", x, y, z, 0, room,
					session.GetHabbo().Id, 0, string.Empty, false);

				if (!room.GetRoomItemHandler().SetFloorItem(session, roomItem, x, y, 0, true, false, true))
				{
					session.GetHabbo().GetInventoryComponent().AddItem(roomItem);
					session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_5"));
				}

				dbClient.RunFastQuery($"DELETE FROM pets_data WHERE id = {moplaId};");
				dbClient.RunFastQuery($"DELETE FROM pets_plants WHERE pet_id = {moplaId};");
			}
		}
	}
}

