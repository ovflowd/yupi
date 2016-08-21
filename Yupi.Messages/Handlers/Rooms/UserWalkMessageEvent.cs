using System;
using Yupi.Model.Domain;
using System.Collections.Generic;



namespace Yupi.Messages.Rooms
{
	public class UserWalkMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int targetX = request.GetInteger();
			int targetY = request.GetInteger();

			RoomEntity entity = session.RoomEntity;

			if (entity == null || !entity.CanWalk || entity.Position.Equals (targetX, targetY)) {
				return;
			}
				
			throw new NotImplementedException ();
			// Teleport
			entity.Position.X = targetX;
			entity.Position.Y = targetY;
			entity.Position.Z = session.Room.HeightMap.GetTileHeight (targetX, targetY);
			var tmp = new List<RoomEntity> ();
			tmp.Add (entity);
			router.GetComposer<UpdateUserStatusMessageComposer> ().Compose (session, tmp);


			/* TODO Implement Horse
			if (entity.RidingHorse) {
				RoomUser roomUserByVirtualId = currentRoom.GetRoomUserManager ().GetRoomUserByVirtualId ((int)roomUserByHabbo.HorseId);

				roomUserByVirtualId.MoveTo (targetX, targetY);
			}*/
		}
	}
}

