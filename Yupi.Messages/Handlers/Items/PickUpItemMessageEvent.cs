using System;




using System.Drawing;


namespace Yupi.Messages.Items
{
	public class PickUpItemMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			// TODO Unused
			request.GetInteger();

			Yupi.Messages.Rooms room = session.GetHabbo().CurrentRoom;

			if (room?.GetRoomItemHandler() == null || session.GetHabbo() == null)
				return;

			RoomItem item = room.GetRoomItemHandler().GetItem(request.GetUInt32());

			if (item == null || item.GetBaseItem().InteractionType == Interaction.PostIt)
				return;

			if (item.UserId != session.GetHabbo().Id && !room.CheckRights(session, true))
				return;

			switch (item.GetBaseItem().InteractionType)
			{
			case Interaction.BreedingTerrier:

				if (room.GetRoomItemHandler().BreedingTerrier.ContainsKey(item.Id))
					room.GetRoomItemHandler().BreedingTerrier.Remove(item.Id);

				foreach (Pet pet in item.PetsList)
				{
					pet.WaitingForBreading = 0;
					pet.BreadingTile = new Point();

					RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);

					if (user == null)
						continue;

					user.Freezed = false;
					room.GetGameMap().AddUserToMap(user, user.Coordinate);

					Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
					user.MoveTo(nextCoord.X, nextCoord.Y);
				}

				item.PetsList.Clear();
				break;

			case Interaction.BreedingBear:

				if (room.GetRoomItemHandler().BreedingBear.ContainsKey(item.Id))
					room.GetRoomItemHandler().BreedingBear.Remove(item.Id);

				foreach (Pet pet in item.PetsList)
				{
					pet.WaitingForBreading = 0;
					pet.BreadingTile = new Point();

					RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);

					if (user == null)
						continue;

					user.Freezed = false;
					room.GetGameMap().AddUserToMap(user, user.Coordinate);

					Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
					user.MoveTo(nextCoord.X, nextCoord.Y);
				}

				item.PetsList.Clear();
				break;
			}
			if (item.IsBuilder)
			{
				using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
				{
					room.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);
					session.GetHabbo().BuildersItemsUsed--;
					router.GetComposer<BuildersClubUpdateFurniCountMessageComposer> ().Compose (session, session.GetHabbo ().BuildersItemsUsed);

					adapter.SetQuery("DELETE FROM items_rooms WHERE id = @item_id");
					adapter.AddParameter("item_id", item.Id);
					adapter.RunQuery ();
				}
			}
			else
			{
				room.GetRoomItemHandler().RemoveFurniture(session, item.Id);

				session.GetHabbo()
					.GetInventoryComponent()
					.AddNewItem(item.Id, item.BaseName, item.ExtraData, item.GroupId, true, true, 0, 0);

				session.GetHabbo().GetInventoryComponent().UpdateItems(false);
			}
		}
	}
}

