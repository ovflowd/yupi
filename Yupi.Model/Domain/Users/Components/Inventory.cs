using System;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Model.Domain.Components
{
	public class Inventory
	{
		[OneToMany]
		public virtual IList<WardrobeItem> Wardrobe { get; protected set; }

		public virtual IList<PetInfo> Pets { get; protected set; }

		public virtual IList<FloorItem> FloorItems { get; protected set; }

		public virtual IList<WallItem> WallItems { get; protected set; }

		public Inventory ()
		{
			this.Wardrobe = new List<WardrobeItem>();
			this.Pets = new List<PetInfo>();
			this.FloorItems = new List<FloorItem>();
			this.WallItems = new List<WallItem>();
		}

		public virtual FloorItem GetFloorItem(int id) {
			return FloorItems.SingleOrDefault (x => x.Id == id);
		}
	}
}

