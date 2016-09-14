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
		private Queue<Vector2> Steps;

		public RoomEntity (Room room, int id)
		{
			this.Id = id;
			this.Room = room;
			Steps = new Queue<Vector2> ();
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

		public bool HasSteps() {
			return Steps.Count > 0;
		}

		public void NextStep() {
			if (!HasSteps ()) {
				throw new InvalidOperationException();
			}

			Vector2 nextStep = Steps.Dequeue ();

			// TODO Be consequent about Vector2 vs Vector3!
			Vector3 nextPos = new Vector3(nextStep.X, nextStep.Y, Room.HeightMap.GetTileHeight ((int)nextStep.X, (int)nextStep.Y));

			if (HasSteps ()) {
				Vector2 move = Steps.Peek ();
				SetRotation(nextPos.ToVector2().CalculateRotation (move));
				Status.SetPosture (new WalkPosture (new Vector3(move.X, move.Y, Room.HeightMap.GetTileHeight ((int)move.X, (int)move.Y))));
			} else {
				Status.SetPosture (StandPosture.Default);
			}

			SetPosition (nextPos);
		}

		public void Walk(Vector2 target) {
			this.Steps = new Queue<Vector2>(Room.Pathfinder.Find (this.Position.ToVector2(), target));
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
			Status.OnUpdateComplete();
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

