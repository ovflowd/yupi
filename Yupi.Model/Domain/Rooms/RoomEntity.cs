using System;
using Yupi.Model.Domain.Components;

namespace Yupi.Model.Domain
{
	[Ignore]
	public abstract class RoomEntity
	{
		public int Id;
		public Vector Position;

		// TODO Use enum
		public int RotHead;
		public int RotBody;

		public abstract EntityType Type { get; }
	}
}

