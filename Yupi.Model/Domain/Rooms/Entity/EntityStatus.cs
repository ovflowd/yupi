using System;
using System.Diagnostics.Contracts;
using System.Text;
using System.Collections.Generic;
using System.Numerics;

namespace Yupi.Model.Domain
{
	[Ignore]
	public abstract class EntityStatus
	{
		public EntityPosture Posture { get; private set; }

		private RoomEntity Entity;

		private List<IStatusString> TemporaryStates;
		private Queue<Vector2> Steps;

		public EntityStatus (RoomEntity entity)
		{
			Contract.Requires (entity != null);
			this.Entity = entity;	
			SetPosture (StandPosture.Default);
			TemporaryStates = new List<IStatusString> ();
		}

		private void SetPosture (EntityPosture posture)
		{
			Contract.Requires (posture != null);
			this.Posture = posture;
			OnChange ();
		}

		internal void OnUpdateComplete() {
			this.TemporaryStates.Clear ();
		}

		public bool IsSitting() {
			return this.Posture is SitPosture;
		}

		public void Stand() {
			SetPosture (StandPosture.Default);
		}

		public void Sit() {
			SetPosture (SitPosture.Default);
		}

		public void Sign(Sign sign) {
			this.TemporaryStates.Add (sign);
			this.OnChange ();
		}

		public override string ToString ()
		{
			return ToStatusString();
		}

		protected void OnChange() {
			this.Entity.ScheduleUpdate();
		}

		protected virtual void GetStates(List<IStatusString> states) {
			states.Add (this.Posture);
			states.AddRange (this.TemporaryStates);
		}

		private string ToStatusString() {
			List<IStatusString> states = new List<IStatusString>();
			GetStates (states);

			List<string> stateStrings = new List<string> (states.Count);

			foreach (IStatusString state in states) {
				string statusStr = state?.ToStatusString ();

				if (!string.IsNullOrEmpty (statusStr)) {
					stateStrings.Add (statusStr);
				}
			}

			return string.Join ("/", stateStrings);
		}
	}
}

