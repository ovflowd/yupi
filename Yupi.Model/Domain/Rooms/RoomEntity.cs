using System;
using Yupi.Model.Domain.Components;

namespace Yupi.Model.Domain
{
	[Ignore]
	public abstract class RoomEntity
	{
		public int Id;
		public Vector Position;

		public abstract EntityType Type { get; }
	}
}

