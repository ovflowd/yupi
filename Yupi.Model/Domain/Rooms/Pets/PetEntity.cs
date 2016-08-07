using System;

namespace Yupi.Model.Domain
{
	public class PetEntity : RoomEntity
	{
		public PetInfo Info { get; set; }

		public override EntityType Type {
			get {
				return EntityType.Pet;
			}
		}
	}
}

