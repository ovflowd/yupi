using System;


namespace Yupi.Messages.Rooms
{
	public class RoomEventUpdateMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			request.GetInteger(); // TODO Unused roomid?

			string name = request.GetString();
			string description = request.GetString();

			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true) || room.RoomData.Event == null)
				return;

			room.RoomData.Event.Name = name;
			room.RoomData.Event.Description = description;

			Yupi.GetGame().GetRoomEvents().UpdateEvent(room.RoomData.Event);
		}
	}
}

