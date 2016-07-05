using System;




namespace Yupi.Messages.Rooms
{
	public class RoomApplySpaceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			UserItem item = session.GetHabbo().GetInventoryComponent().GetItem(request.GetUInt32());

			if (item == null)
				return;

			// TODO Improve handling of type
			RoomSpacesMessageComposer.Type type = RoomSpacesMessageComposer.Type.FLOOR;

			if (item.BaseItem.Name.ToLower().Contains("wallpaper"))
				type = RoomSpacesMessageComposer.Type.WALLPAPER;
			else if (item.BaseItem.Name.ToLower().Contains("landscape"))
				type = RoomSpacesMessageComposer.Type.LANDSCAPE;

			switch (type)
			{
			case RoomSpacesMessageComposer.Type.FLOOR:

				room.RoomData.Floor = item.ExtraData;

				Yupi.GetGame()
					.GetAchievementManager()
					.ProgressUserAchievement(session, "ACH_RoomDecoFloor", 1);
				break;

			case RoomSpacesMessageComposer.Type.WALLPAPER:

				room.RoomData.WallPaper = item.ExtraData;

				Yupi.GetGame()
					.GetAchievementManager()
					.ProgressUserAchievement(session, "ACH_RoomDecoWallpaper", 1);
				break;

			case RoomSpacesMessageComposer.Type.LANDSCAPE:

				room.RoomData.LandScape = item.ExtraData;
				// TODO Handle Achivements eventbased?
				Yupi.GetGame()
					.GetAchievementManager()
					.ProgressUserAchievement(session, "ACH_RoomDecoLandscape", 1);
				break;
			}

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery("UPDATE rooms_data SET " + type.ToString().ToLower() + " = @extradata WHERE id = @room");
				queryReactor.AddParameter("extradata", item.ExtraData);
				queryReactor.AddParameter("room", room.RoomId);
				queryReactor.RunQuery();

				queryReactor.SetQuery("DELETE FROM items_rooms WHERE id=@id");
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();
			}

			session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);

			router.GetComposer<RoomSpacesMessageComposer> ().Compose (room, type, room.RoomData);
		}
	}
}

