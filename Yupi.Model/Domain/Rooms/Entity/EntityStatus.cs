using System;
using System.Diagnostics.Contracts;
using System.Text;
using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	[Ignore]
	public abstract class EntityStatus
	{
		public EntityPosture Posture { get; private set; }

		private RoomEntity Entity;

		private List<IStatusString> States;

		public EntityStatus (RoomEntity entity)
		{
			Contract.Requires (entity != null);
			this.Entity = entity;	
			SetPosture (StandPosture.Default);
			States = new List<IStatusString> ();
			RegisterStatus (this.Posture);
		}

		protected void RegisterStatus(params IStatusString[] status) {
			States.AddRange (status);
		}

		private void SetPosture (EntityPosture posture)
		{
			Contract.Requires (posture != null);
			this.Posture = posture;
			this.Entity.ScheduleUpdate();
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

		public override string ToString ()
		{
			return ToStatusString(this.Posture);
		}

		private string ToStatusString(params IStatusString[] states) {
			List<string> stateStrings = new List<string> (states.Length);

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

