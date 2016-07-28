using System;



namespace Yupi.Messages.Rooms
{
	public class UserDanceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo == null)
				return;

			roomUserByHabbo.UnIdle();

			uint danceId = request.GetUInt32();

			if (danceId > 4)
				danceId = 0;

			if (danceId > 0 && roomUserByHabbo.CarryItemId > 0)
				roomUserByHabbo.CarryItem(0);

			roomUserByHabbo.DanceId = danceId;

			router.GetComposer<DanceStatusMessageComposer> ().Compose (room, roomUserByHabbo.VirtualId, danceId);
		}
	}
}

