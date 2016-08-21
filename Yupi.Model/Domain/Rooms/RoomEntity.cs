using System;
using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Model.Domain
{
	[Ignore]
	public abstract class RoomEntity : ISender
	{
		public int Id;
		public Vector3D Position;

		// TODO Use enum
		public int RotHead;
		public int RotBody;
		public Room Room { get; private set; }
		public bool CanWalk { get; private set; }

		public abstract EntityType Type { get; }

		public RoomEntity (Room room)
		{
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
	}
}

