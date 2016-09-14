using System;

namespace Yupi.Model.Domain
{
	public class FootballGateBaseItem : FloorBaseItem
	{
		// TODO Should be enum
		public virtual int Color { get; set; }

		public override Item CreateNew ()
		{
			return new FootballGateItem () {
				BaseItem = this
			};
		}
	}
}

