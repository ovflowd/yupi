using System;


namespace Yupi.Messages.Items
{
	public class GiveHanditemMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo == null)
				return;

			RoomUser roomUserByHabbo2 = room.GetRoomUserManager().GetRoomUserByHabbo(request.GetUInt32());

			if (roomUserByHabbo2 == null)
				return;

			// TODO Create method to calculate distance
			if ((!(
				Math.Abs(roomUserByHabbo.X - roomUserByHabbo2.X) < 3 &&
				Math.Abs(roomUserByHabbo.Y - roomUserByHabbo2.Y) < 3) &&
				roomUserByHabbo.GetClient().GetHabbo().Rank <= 4u) || roomUserByHabbo.CarryItemId <= 0 ||
				roomUserByHabbo.CarryTimer <= 0)
				return;

			roomUserByHabbo2.CarryItem(roomUserByHabbo.CarryItemId);
			roomUserByHabbo.CarryItem(0);
			roomUserByHabbo2.DanceId = 0;
		}
	}
}

