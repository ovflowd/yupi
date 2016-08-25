using System;
using System.Numerics;




namespace Yupi.Messages.Rooms
{
	public class LookAtUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int x = request.GetInteger();
			int y = request.GetInteger();

			if (session.RoomEntity == null || session.RoomEntity.Position == new Vector3(x, y, 0)) {
				return;
			}

			session.RoomEntity.UnIdle();

			int rotation = PathFinder.CalculateRotation(roomUserByHabbo.X, roomUserByHabbo.Y, x, y);

			session.RoomEntity.SetRot(rotation, false);

			// TODO Implement Horse
			/*
			if (!roomUserByHabbo.RidingHorse)
				return;

			RoomUser roomUserByVirtualId = session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByVirtualId(Convert.ToInt32(roomUserByHabbo.HorseId));

			roomUserByVirtualId.SetRot(rotation, false);
			roomUserByVirtualId.UpdateNeeded = true;
			*/
		}
	}
}

