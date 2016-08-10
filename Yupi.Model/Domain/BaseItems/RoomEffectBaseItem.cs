using System;
using Yupi.Model.Domain;

namespace Yupi.Model
{
	public class RoomEffectBaseItem : FloorBaseItem
	{
		public override Item CreateNew ()
		{
			return new RoomEffectItem ();
		}
	}
}

