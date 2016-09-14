using System;




using System.Drawing;
using Yupi.Protocol;
using Yupi.Model.Domain;






namespace Yupi.Messages.Items
{
	// TODO Potentially blob class...
	public class TriggerItemMessageEvent : AbstractHandler
	{
		/*
		 * TODO
		 * Also handles the following: UseHabboWheelMessageEvent, TriggerWallItemMessageEvent, EnterOneWayDoorMessageEvent, TriggerDiceRollMessageEvent
		 */

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

			if (room == null)
				return;

			int num = request.GetInteger();

			if (num < 0)
				return;

			uint pId = Convert.ToUInt32(num);

			RoomItem item = room.GetRoomItemHandler().GetItem(pId);

			if (item == null)
				return;

			bool hasRightsOne = room.CheckRights(session, false, true);
			bool hasRightsTwo = room.CheckRights(session, true);

			switch (item.GetBaseItem().InteractionType)
			{
			case Interaction.RoomBg:
				{
					if (!hasRightsTwo)
						return;

					room.TonerData.Enabled = room.TonerData.Enabled == 0 ? 1 : 0;
					router.GetComposer<UpdateRoomItemMessageComposer> ().Compose (room, item);

					item.UpdateState();

					using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
						queryReactor.RunFastQuery(
							$"UPDATE items_toners SET enabled = '{room.TonerData.Enabled}' LIMIT 1");

					return;
				}
			case Interaction.LoveShuffler:
			case Interaction.LoveLock:
				{
					if (!hasRightsOne)
						return;

					TriggerLoveLock(router, session, item);

					return;
				}
			case Interaction.Moplaseed:
			case Interaction.RareMoplaSeed:
				{
					if (!hasRightsOne)
						return;

					PlantMonsterplant(router, session, item, room);

					return;
				}
			}

			item.Interactor.OnTrigger(session, item, request.GetInteger(), hasRightsOne);
			item.OnTrigger(room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id));

			foreach (RoomUser current in room.GetRoomUserManager().UserList.Values.Where(current => current != null))
				room.GetRoomUserManager().UpdateUserStatus(current, true);
				*/
			throw new NotImplementedException ();
		}

		private void PlantMonsterplant(IRouter router,  Yupi.Protocol.ISender session, FloorItem<MonsterPlantBaseItem> mopla, RoomData room)
		{
			/*
			int rarity = 0, internalRarity;

			if (room == null || mopla == null)
				return;

			if ((mopla.GetBaseItem().InteractionType != Interaction.Moplaseed) &&
				(mopla.GetBaseItem().InteractionType != Interaction.RareMoplaSeed))
				return;

			if (string.IsNullOrEmpty(mopla.ExtraData) || mopla.ExtraData == "0")
				rarity = 1;

			if (!string.IsNullOrEmpty(mopla.ExtraData) && mopla.ExtraData != "0")
				rarity = int.TryParse(mopla.ExtraData, out internalRarity) ? internalRarity : 1;

			int getX = mopla.X;
			int getY = mopla.Y;

			room.GetRoomItemHandler().RemoveFurniture(session, mopla.Id, false);

			Pet pet = CatalogManager.CreatePet(session.GetHabbo().Id, "Monsterplant", "pet_monster", "0", "0", rarity);

			router.GetComposer<SendMonsterplantIdMessageComposer> ().Compose (session, pet.PetId);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
				queryReactor.SetQuery("UPDATE pets_data SET room_id = @room, x = @x, y = @y WHERE id = @id");
				queryReactor.AddParameter("x", getX);
				queryReactor.AddParameter("y", getY);
				queryReactor.AddParameter("id", pet.PetId);
				queryReactor.RunQuery ();
			}

			pet.PlacedInRoom = true;
			pet.RoomId = room.RoomId;

			RoomBot bot = new RoomBot(pet.PetId, pet.OwnerId, pet.RoomId, AiType.Pet, "freeroam", pet.Name, "", pet.Look, getX, getY, 0.0, 4, null, null, "", 0, "");

			room.GetRoomUserManager().DeployBot(bot, pet);

			if (pet.DbState != DatabaseUpdateState.NeedsInsert)
				pet.DbState = DatabaseUpdateState.NeedsUpdate;

			using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryreactor2.SetQuery("DELETE FROM items_rooms WHERE id = @id");
				queryreactor2.AddParameter("id", mopla.Id);
				queryreactor2.RunQuery ();

				room.GetRoomUserManager().SavePets(queryreactor2);
			}*/
			throw new NotImplementedException ();
		}

		private void TriggerLoveLock(IRouter router, UserInfo session, LovelockItem loveLock)
		{
			/*
			string[] loveLockParams = loveLock.ExtraData.Split(Convert.ToChar(5));

			try
			{
				if (loveLockParams[0] == "1")
					return;

				Point pointOne;
				Point pointTwo;

				switch (loveLock.Rot)
				{
				case 2:
					pointOne = new Point(loveLock.X, loveLock.Y + 1);
					pointTwo = new Point(loveLock.X, loveLock.Y - 1);
					break;

				case 4:
					pointOne = new Point(loveLock.X - 1, loveLock.Y);
					pointTwo = new Point(loveLock.X + 1, loveLock.Y);
					break;

				default:
					return;
				}

				RoomUser roomUserOne = loveLock.GetRoom().GetRoomUserManager().GetUserForSquare(pointOne.X, pointOne.Y);
				RoomUser roomUserTwo = loveLock.GetRoom().GetRoomUserManager().GetUserForSquare(pointTwo.X, pointTwo.Y);

				RoomUser user = loveLock.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

				if (roomUserOne == null || roomUserTwo == null)
				{
					user.MoveTo(loveLock.X, loveLock.Y + 1);
					return;
				}

				if (roomUserOne.GetClient() == null || roomUserTwo.GetClient() == null)
				{
					session.SendNotif(Yupi.GetLanguage().GetVar("lovelock_error_2"));
					return;
				}

				roomUserOne.CanWalk = false;
				roomUserTwo.CanWalk = false;

				loveLock.InteractingUser = roomUserOne.GetClient().GetHabbo().Id;
				loveLock.InteractingUser2 = roomUserTwo.GetClient().GetHabbo().Id;

				router.GetComposer<LoveLockDialogueMessageComposer>().Compose(roomUserOne.GetClient(), roomUserTwo.GetClient());
			}
			catch
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("lovelock_error_3"));
			}*/
			throw new NotImplementedException ();
		}
	}
}

