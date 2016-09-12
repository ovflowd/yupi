using System;

namespace Yupi.Model.Domain
{
	[Ignore]
	public abstract class HumanEntity : RoomEntity
	{
		public HumanStatus HumanStatus { get; private set; }

		public override EntityStatus Status {
			get {
				return HumanStatus;
			}
		}

		public HumanEntity (Room room, int id) : base (room, id)
		{
			HumanStatus = new HumanStatus (this);
		}
	}
}

