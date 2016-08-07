using System;

namespace Yupi.Model.Domain
{
	public class PetBaseItem : FloorBaseItem
	{
		public override ItemType Type {
			get {
				return ItemType.Pet;
			}
		}
	}
}

