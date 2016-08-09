using System;

namespace Yupi.Model.Domain
{
	public abstract class FloorItem<T> : Item<T>, IFloorItem where T : FloorBaseItem
	{
		
	}
}

