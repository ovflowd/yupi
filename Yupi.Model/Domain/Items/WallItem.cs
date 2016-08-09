using System;
using Yupi.Model.Domain.Components;

namespace Yupi.Model.Domain
{
	public abstract class WallItem<T> : Item<T>, IWallItem where T : WallBaseItem
	{
		public virtual WallCoordinate Position { get; protected set; }
	}
}

