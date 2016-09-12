using System;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class HumanStatus : EntityStatus
	{
		public RoomRightLevel Rights { get; private set; }

		public HumanStatus (HumanEntity entity) : base(entity)
		{
			SetRights ();
			RegisterStatus (Rights);
		}

		protected virtual void SetRights() {
			Rights = RoomRightLevel.None;
			// TODO Implement
		}
	}
}

