using System;

using System.Collections.Generic;

namespace Yupi.Messages.Rooms
{
	public class GetRoomBannedUsersMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint roomId = request.GetUInt32();

			Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

			if (room == null)
				return;

			router.GetComposer<RoomBannedListMessageComposer> ().Compose (session, roomId, room.BannedUsers());
		}
	}
}

