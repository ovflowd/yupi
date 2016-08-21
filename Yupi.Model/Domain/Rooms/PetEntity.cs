using System;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class PetEntity : RoomEntity
	{
		public PetInfo Info { get; set; }

		public override EntityType Type {
			get {
				return EntityType.Pet;
			}
		}

		public PetEntity (Room room) : base(room)
		{

		}
	}
}

