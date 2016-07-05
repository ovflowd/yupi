using System;



namespace Yupi.Messages.Rooms
{
	public class UserWalkMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room currentRoom = session.GetHabbo().CurrentRoom;

			RoomUser roomUserByHabbo = currentRoom?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo == null || !roomUserByHabbo.CanWalk)
				return;

			int targetX = request.GetInteger();
			int targetY = request.GetInteger();

			if (targetX == roomUserByHabbo.X && targetY == roomUserByHabbo.Y)
				return;

			roomUserByHabbo.MoveTo(targetX, targetY);

			if (!roomUserByHabbo.RidingHorse)
				return;

			RoomUser roomUserByVirtualId = currentRoom.GetRoomUserManager().GetRoomUserByVirtualId((int) roomUserByHabbo.HorseId);

			roomUserByVirtualId.MoveTo(targetX, targetY);
		}
	}
}

