using System;

namespace Yupi.Model.Domain
{
	public class FootballGateItem : FloorItem<FootballGateBaseItem>
	{
		public virtual string LookMale { get; set; }
		public virtual string LookFemale { get; set; }

		public FootballGateItem ()
		{
			LookMale = "lg-270-82.ch-210-66";
			LookFemale = "lg-270-82.ch-210-66";
		}

		public override string GetExtraData ()
		{
			return String.Join (";", LookMale, LookFemale);
		}
	}
}

