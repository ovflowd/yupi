using System;




namespace Yupi.Messages.Items
{
	public class PlaceBuildersFurniture : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			request.GetInteger(); // pageId
			uint itemId = Convert.ToUInt32(request.GetInteger());
			string extradata = request.GetString();
			int x = request.GetInteger();
			int y = request.GetInteger();
			int dir = request.GetInteger();
			Yupi.Messages.Rooms actualRoom = session.GetHabbo().CurrentRoom;
			CatalogItem item = Yupi.GetGame().GetCatalogManager().GetItem(itemId);

			if (actualRoom == null || item == null)
				return;

			session.GetHabbo().BuildersItemsUsed++;

			router.GetComposer<BuildersClubUpdateFurniCountMessageComposer> ().Compose (session, session.GetHabbo ().BuildersItemsUsed);

			double z = actualRoom.GetGameMap().SqAbsoluteHeight(x, y);
			using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				adapter.SetQuery(
					"INSERT INTO items_rooms (user_id,room_id,item_name,x,y,z,rot,builders) VALUES (@userId,@roomId,@itemName,@x,@y,@z,@rot,'1')");
				adapter.AddParameter("userId", session.GetHabbo().Id);
				adapter.AddParameter("roomId", actualRoom.RoomId);
				adapter.AddParameter("itemName", item.BaseName);
				adapter.AddParameter("x", x);
				adapter.AddParameter("y", y);
				adapter.AddParameter("z", z);
				adapter.AddParameter("rot", dir);

				uint insertId = (uint) adapter.InsertQuery();

				RoomItem newItem = new RoomItem(insertId, actualRoom.RoomId, item.BaseName, extradata, x, y, z, dir,
					actualRoom, session.GetHabbo().Id, 0, string.Empty, true);

				session.GetHabbo().BuildersItemsUsed++;

				actualRoom.GetRoomItemHandler().FloorItems.TryAdd(newItem.Id, newItem);

				router.GetComposer<AddFloorItemMessageComposer> ().Compose (actualRoom, newItem);
				actualRoom.GetGameMap().AddItemToMap(newItem);
			}
		}
	}
}

