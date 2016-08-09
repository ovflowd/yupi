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

		public virtual IList<IFloorItem> FloorItems { get; protected set; }

		public virtual IList<IWallItem> WallItems { get; protected set; }

		public Inventory ()
		{
			this.Wardrobe = new List<WardrobeItem>();
			this.Pets = new List<PetInfo>();
			this.FloorItems = new List<IFloorItem>();
			this.WallItems = new List<IWallItem>();
		}

		public virtual IFloorItem GetFloorItem(int id) {
			return FloorItems.SingleOrDefault (x => x.Id == id);
		}
	}
}

