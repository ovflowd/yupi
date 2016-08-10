using System;

namespace Yupi.Model.Domain
{
	public class WallBaseItem : BaseItem
	{
		public override ItemType Type {
			get {
				return ItemType.Wall;
			}
		}

		public override Item CreateNew ()
		{
			return new SimpleWallItem () {
				BaseItem = this
			};
		}
	}
}

