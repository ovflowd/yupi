using System;
using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	public class VendingItem : FloorItem
	{
		public virtual IList<int> VendingIds { get; protected set; }

		public VendingItem() {
			VendingIds = new List<int>();
		}
	}
}

