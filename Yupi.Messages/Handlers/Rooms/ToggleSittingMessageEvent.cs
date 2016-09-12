using System;
using System.Numerics;
using Yupi.Model.Domain;


namespace Yupi.Messages.Rooms
{
	public class ToggleSittingMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.RoomEntity == null) {
				return;
			}

			if (session.RoomEntity.RotBody % 2 != 0) {
				session.RoomEntity.SetRotation (session.RoomEntity.RotBody - 1);
			}

			Vector3 position = session.RoomEntity.Position;
			position.Z = session.Room.HeightMap.GetTileHeight (session.RoomEntity.Position);
			session.RoomEntity.SetPosition (position);

			EntityStatus status = session.RoomEntity.Status;

			if (status.IsSitting ()) {
				status.Stand ();
			} else {
				// TODO Stop dancing
				status.Sit();
			}
		}
	}
}

