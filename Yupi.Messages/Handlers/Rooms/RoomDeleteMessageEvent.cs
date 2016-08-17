using System;




namespace Yupi.Messages.Rooms
{
	public class RoomDeleteMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint roomId = request.GetUInt32();

			/*
			if (session.GetHabbo().UsersRooms == null)
				return;

			Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

			if (room == null)
				return;

			if (room.RoomData.Owner != session.GetHabbo().UserName && session.GetHabbo().Rank <= 6u)
				return;

			if (session.GetHabbo().GetInventoryComponent() != null)
				session.GetHabbo().GetInventoryComponent().AddItemArray(room.GetRoomItemHandler().RemoveAllFurniture(session));

			RoomData roomData = room.RoomData;

			Yupi.GetGame().GetRoomManager().UnloadRoom(room, "Delete room");
			Yupi.GetGame().GetRoomManager().QueueVoteRemove(roomData);

			if (roomData == null)
				return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.RunFastQuery($"DELETE FROM rooms_data WHERE id = {roomId}");
				queryReactor.RunFastQuery($"DELETE FROM users_favorites WHERE room_id = {roomId}");
				queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE room_id = {roomId}");
				queryReactor.RunFastQuery($"DELETE FROM rooms_rights WHERE room_id = {roomId}");
				queryReactor.RunFastQuery($"UPDATE users SET home_room = '0' WHERE home_room = {roomId}");
			}

			// TODO Remove those damn $ strings...
			if (session.GetHabbo().Rank > 5u && session.GetHabbo().UserName != roomData.Owner)
				Yupi.GetGame().GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, roomData.Name, "Room deletion", $"Deleted room ID {roomData.Id}");

			RoomData roomData2 = (from p in session.GetHabbo().UsersRooms where p.Id == roomId select p).SingleOrDefault();

			if (roomData2 != null)
				session.GetHabbo().UsersRooms.Remove(roomData2);
				*/
			throw new NotImplementedException ();
		}
	}
}

