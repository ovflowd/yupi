using System;



namespace Yupi.Messages.Items
{
	public class RoomAddPostItMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			
			if (session.Room == null || !session.Room.Data.HasRights (session.Info))
				return;

			int id = request.GetInteger ();
			string locationData = request.GetString ();

			/*
			UserItem item = session.GetHabbo ().GetInventoryComponent ().GetItem (id);

			if (item == null)
				return;

			WallCoordinate wallCoord = new WallCoordinate (":" + locationData.Split (':') [1]);

			RoomItem item2 = new RoomItem (item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData, wallCoord, room,
				                  session.GetHabbo ().Id, item.GroupId, false);

			if (room.GetRoomItemHandler ().SetWallItem (session, item2))
				session.GetHabbo ().GetInventoryComponent ().RemoveItem (id, true);
*/
			throw new NotImplementedException ();
		}
	}
}

