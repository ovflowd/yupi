using System;

using System.Data;



namespace Yupi.Messages.Rooms
{
	public class RoomRemoveAllRightsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			DataTable table;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery("SELECT user_id FROM rooms_rights WHERE room_id=@room");
				queryReactor.AddParameter("room", room.RoomId);
				table = queryReactor.GetTable();
			}

			RemoveRightsMessageComposer removeRightsComposer = router.GetComposer<RemoveRightsMessageComposer> ();

			foreach (DataRow dataRow in table.Rows)
			{
				uint userId = (uint) dataRow[0];

				RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(userId);

				removeRightsComposer.Compose (session, room.RoomId, userId);


				if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
					continue;

				router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (roomUserByHabbo.GetClient (), 0);
				roomUserByHabbo.RemoveStatus("flatctrl 1");
				roomUserByHabbo.UpdateNeeded = true;
			}

			using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				// TODO Replace interpreted strings (bad practice for SQL!!!)
				queryreactor2.RunFastQuery ($"DELETE FROM rooms_rights WHERE room_id = {room.RoomId}");
			}

			room.UsersWithRights.Clear();

			UsersWithRights();
			*/
			throw new NotImplementedException ();
		}
	}
}

