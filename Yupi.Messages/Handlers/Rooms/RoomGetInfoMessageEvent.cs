using System;

using System.Data;


namespace Yupi.Messages.Rooms
{
	public class RoomGetInfoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint id = request.GetUInt32();

			// TODO num & num2 ???
			int num = request.GetInteger();
			int num2 = request.GetInteger();

			Room room = Yupi.GetGame().GetRoomManager().LoadRoom(id);

			if (room == null || room.RoomData == null)
				return;
			
			bool show = !(num == 0 && num2 == 1);

			router.GetComposer<RoomDataMessageComposer> ().Compose (session, room, show, true);
			router.GetComposer<LoadRoomRightsListMessageComposer> ().Compose (session, room);
		}
	}
}

