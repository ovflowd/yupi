using System;


namespace Yupi.Messages.Rooms
{
	public class RoomUnbanUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint userId = request.GetUInt32();
			uint roomId = request.GetUInt32();
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

			if (room == null)
				return;

			room.Unban(userId);

			router.GetComposer<RoomUnbanUserMessageComposer> ().Compose (session, roomId, userId);
		}
	}
}

