using System;



namespace Yupi.Messages.Items
{
	public class RoomAddPostItMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session))
				return;

			uint id = request.GetUInt32();
			string locationData = request.GetString();

			UserItem item = session.GetHabbo().GetInventoryComponent().GetItem(id);

			if (item == null)
				return;

			try
			{
				WallCoordinate wallCoord = new WallCoordinate(":" + locationData.Split(':')[1]);

				RoomItem item2 = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData, wallCoord, room,
					session.GetHabbo().Id, item.GroupId, false);

				if (room.GetRoomItemHandler().SetWallItem(session, item2))
					session.GetHabbo().GetInventoryComponent().RemoveItem(id, true);
			}
			catch
			{
				// ignored
				// TODO Why?
			}
		}
	}
}

