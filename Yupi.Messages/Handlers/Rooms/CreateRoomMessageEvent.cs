using System;


namespace Yupi.Messages.Rooms
{
	public class CreateRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			// TODO Magic number!
			if (session.GetHabbo().UsersRooms.Count >= 75)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("user_has_more_then_75_rooms"));

				return;
			}

			if (Yupi.GetUnixTimeStamp() - session.GetHabbo().LastSqlQuery < 20)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("user_create_room_flood_error"));
				return;
			}

			string name = request.GetString();
			string description = request.GetString();
			string roomModel = request.GetString();
			int category = request.GetInteger();
			int maxVisitors = request.GetInteger();
			int tradeState = request.GetInteger();

			RoomData data = Yupi.GetGame().GetRoomManager().CreateRoom(session, name, description, roomModel, category, maxVisitors, tradeState);

			if (data == null)
				return;

			session.GetHabbo().LastSqlQuery = Yupi.GetUnixTimeStamp();
			router.GetComposer<OnCreateRoomInfoMessageComposer> ().Compose (session, data);
		}
	}
}

