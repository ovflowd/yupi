using System;

namespace Yupi.Model.Domain
{
	public class BadgeDisplayBaseItem : FloorBaseItem
	{
		public override Item CreateNew ()
		{
			return new BadgeDisplayItem () {
				BaseItem = this
			};
		}
	}
}

