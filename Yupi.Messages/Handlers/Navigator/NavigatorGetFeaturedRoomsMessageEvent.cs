using System;


namespace Yupi.Messages.Navigator
{
	public class NavigatorGetFeaturedRoomsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint roomId = request.GetUInt32();

			RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);

			if (roomData == null)
				return;

			router.GetComposer<OfficialRoomsMessageComposer> ().Compose (session, roomData);
		}
	}
}

