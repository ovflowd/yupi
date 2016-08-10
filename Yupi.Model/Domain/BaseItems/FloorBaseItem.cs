using System;

namespace Yupi.Model.Domain
{
	public class FloorBaseItem : BaseItem
	{
		public override Item CreateNew ()
		{
			return new SimpleFloorItem () {
				BaseItem = this
			};
		}
	}
}

