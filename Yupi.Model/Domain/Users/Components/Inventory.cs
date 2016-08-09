using System;
using System.Collections.Generic;

namespace Yupi.Model.Domain.Components
{
	public class Inventory
	{
		[OneToMany]
		public virtual IList<WardrobeItem> Wardrobe { get; protected set; }
		public virtual IList<PetInfo> Pets { get; protected set; }

		public Inventory ()
		{
			this.Wardrobe = new List<WardrobeItem>();
			this.Pets = new List<PetInfo>();
		}
		
	}
}

