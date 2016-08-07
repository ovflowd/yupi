using System;

namespace Yupi.Model.Domain
{
	public class BotEntity : RoomEntity
	{
		public BotInfo Info { get; set; }

		public override EntityType Type {
			get {
				return EntityType.Bot;
			}
		}
	}
}

