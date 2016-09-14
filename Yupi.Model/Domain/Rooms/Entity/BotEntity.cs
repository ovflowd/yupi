using System;
using Yupi.Model.Domain;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class BotEntity : HumanEntity
	{
		public BotInfo Info { get; set; }

		public override EntityType Type {
			get {
				return EntityType.Bot;
			}
		}

		public override BaseInfo BaseInfo {
			get {
				return Info;
			}
		}

		public BotEntity (Room room, int id) : base(room, id)
		{
			
		}
	}
}

