using System;


namespace Yupi.Messages.Rooms
{
	public class RoomGetFilterMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint roomId = request.GetUInt32();

			/*
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			router.GetComposer<RoomLoadFilterMessageComposer> ().Compose (session, room.WordFilter);
			*/
			throw new NotImplementedException ();
		}
	}
}

