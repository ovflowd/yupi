using System;
using Yupi.Model.Domain.Components;
using Yupi.Protocol;
using System.Numerics;

namespace Yupi.Model.Domain
{
	[Ignore]
	public abstract class RoomEntity : ISender
	{
		public int Id;
		public Vector3 Position;

		// TODO Use enum
		public int RotHead { get; private set; }
		public int RotBody { get; private set; }
		public Room Room { get; private set; }
		public bool CanWalk { get; private set; }
		public bool NeedsUpdate { get; private set; }

		public abstract EntityType Type { get; }

		public RoomEntity (Room room, int id)
		{
			this.Id = id;
			this.Room = room;
			this.CanWalk = true;
		}

		public virtual void Send (Yupi.Protocol.Buffers.ServerMessage message)
		{
			// Do nothing
		}

		public virtual void OnRoomExit () {
			// Do nothing
		}

		public void SetRotation(int rotation) {
			this.RotBody = rotation;
			this.RotHead = rotation;
			ScheduleUpdate ();
		}

		public void ScheduleUpdate() {
			NeedsUpdate = true;
		}

		public void UpdateComplete() {
			NeedsUpdate = false;
		}
	}
}

