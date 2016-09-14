using System;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class PetEntity : RoomEntity
	{
		public PetInfo Info { get; set; }
		public PetStatus PetStatus { get; private set; }

		public override EntityType Type {
			get {
				return EntityType.Pet;
			}
		}

		public override BaseInfo BaseInfo {
			get {
				return Info;
			}
		}

		public override EntityStatus Status {
			get {
				return PetStatus;
			}
		}

		public PetEntity (Room room, int id) : base(room, id)
		{
			PetStatus = new PetStatus (this);
		}
	}
}

