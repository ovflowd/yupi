using System;

namespace Yupi.Messages.Items
{
	public class OpenPostItMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
			RoomItem item = room?.GetRoomItemHandler().GetItem(request.GetUInt32());

			router.GetComposer<LoadPostItMessageComposer> ().Compose (session, item);
		}
	}
}

