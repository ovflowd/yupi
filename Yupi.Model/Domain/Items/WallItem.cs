using System;
using Yupi.Model.Domain.Components;

namespace Yupi.Model.Domain
{
	public class WallItem : Item<WallBaseItem>
	{
		public virtual WallCoordinate Position { get; protected set; }
	}
}

