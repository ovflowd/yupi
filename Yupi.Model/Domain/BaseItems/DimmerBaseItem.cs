using System;

namespace Yupi.Model.Domain
{   // TODO Consistency: Moodlight vs Dimmer
	public class DimmerBaseItem : WallBaseItem
	{
		public override Item CreateNew ()
		{
			return new DimmerItem () {
				BaseItem = this
			};
		}
	}
}

