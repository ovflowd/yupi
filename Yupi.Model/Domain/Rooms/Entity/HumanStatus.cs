using System;
using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class HumanStatus : EntityStatus
	{
		public RoomRightLevel Rights { get; private set; }

		public HumanStatus (HumanEntity entity) : base(entity)
		{
			SetRights ();
		}

		protected override void GetStates (List<IStatusString> states)
		{
			base.GetStates (states);
			states.Add (this.Rights);
		}

		protected virtual void SetRights() {
			Rights = RoomRightLevel.None;
			// TODO Implement
		}
	}
}

