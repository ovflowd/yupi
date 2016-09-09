using System;
using Yupi.Model.Domain;
using System.Collections.Generic;
using System.Numerics;



namespace Yupi.Messages.Rooms
{
	public class UserWalkMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int targetX = request.GetInteger();
			int targetY = request.GetInteger();

			RoomEntity entity = session.RoomEntity;

			if (entity == null || !entity.CanWalk || entity.Position.Equals(new Vector2 (targetX, targetY))) {
				return;
			}

			// TODO Implement pathfinder
			// Teleport
			Vector3 position = entity.Position;
			position.X = targetX;
			position.Y = targetY;
			position.Z = session.Room.HeightMap.GetTileHeight (targetX, targetY);
			entity.SetPosition (position);


			/* TODO Implement Horse
			if (entity.RidingHorse) {
				RoomUser roomUserByVirtualId = currentRoom.GetRoomUserManager ().GetRoomUserByVirtualId ((int)roomUserByHabbo.HorseId);

				roomUserByVirtualId.MoveTo (targetX, targetY);
			}*/
		}
	}
}

