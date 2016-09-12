using System;
using Yupi.Model.Domain.Components;
using Yupi.Protocol;
using System.Numerics;
using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	[Ignore]
	public abstract class RoomEntity
	{
		public int Id;
		public Vector3 Position { get; private set; }

		// TODO Use enum
		public int RotHead { get; private set; }
		public int RotBody { get; private set; }
		public Room Room { get; private set; }
		public bool NeedsUpdate { get; private set; }
		public bool IsAsleep { get; private set; }

		public abstract EntityType Type { get; }
		public abstract BaseInfo BaseInfo { get; }
		public abstract EntityStatus Status { get; }

		[Ignore]
		public delegate void OnSleepChange(RoomEntity entity);
		public OnSleepChange OnSleepChangeCB { get; set; }

		public RoomEntity (Room room, int id)
		{
			this.Id = id;
			this.Room = room;
		}

		public virtual void OnRoomExit () {
			// Do nothing
		}

		public virtual void HandleChatMessage(UserEntity user, Action<Habbo> sendTo) {
			// TODO Implement Tent
			// TODO Implement Distance?

			int rotation = Position.CalculateRotation (user.Position);
			// TODO Should only be temporary
			// TODO Add distance calculation!
			SetHeadRotation (rotation);
		}

		public bool CanWalk() {
			// TODO Implement
			return true;
		}

		public void SetHeadRotation(int rotation) {
			if (rotation < 0 || rotation > 7) {
				throw new ArgumentOutOfRangeException ("rotation");
			}

			int delta = this.RotBody - rotation;
			this.RotHead = (this.RotBody - Math.Sign(delta)) % 8;
			ScheduleUpdate ();
		}

		public void SetRotation(int rotation) {
			if (rotation < 0 || rotation > 7) {
				throw new ArgumentOutOfRangeException ("rotation");
			}

			this.RotBody = rotation;
			this.RotHead = rotation;
			ScheduleUpdate ();
		}

		public void SetPosition(Vector3 newPosition) {
			this.Position = newPosition;
			ScheduleUpdate ();
		}

		internal void ScheduleUpdate() {
			NeedsUpdate = true;
		}

		internal void UpdateComplete() {
			NeedsUpdate = false;
		}

		public void Sleep() {
			if (!this.IsAsleep) {
				this.IsAsleep = true;
				OnSleepChangeCB (this);
			}
		}

		public void Wake() {
			if (this.IsAsleep) {
				this.IsAsleep = false;
				OnSleepChangeCB (this);
			}
		}
	}
}

