using System;
using System.Numerics;
using Yupi.Model;




namespace Yupi.Messages.Rooms
{
	public class LookAtUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int x = request.GetInteger ();
			int y = request.GetInteger ();

			if (session.RoomEntity == null) {
				return;
			}

			Vector2 target = new Vector2 (x, y);
			Vector2 position = new Vector2 (
				session.RoomEntity.Position.X, 
				session.RoomEntity.Position.Y
			);

			if (position == target) {
				return;
			}

			int rotation = position.CalculateRotation(target);

			session.RoomEntity.SetRotation (rotation);

			// TODO Implement
			//session.RoomEntity.UnIdle();

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

