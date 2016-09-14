using System;

namespace Yupi.Model.Domain
{
	public class MonsterPlantSeedItem : FloorItem<MonsterPlantSeedBaseItem>
	{
		// TODO Is this correct?
		public virtual int Race { get; set; }

		[Ignore]
		private static Random Rand = new Random();

		public override void TryParseExtraData (string data)
		{
			Race = Rand.Next (12);
		}

		public override string GetExtraData ()
		{
			return Race.ToString ();
		}
	}
}

