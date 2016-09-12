using System;

namespace Yupi.Model
{
	public class StandPosture : EntityPosture
	{
		public static readonly StandPosture Default = new StandPosture();

		private StandPosture ()
		{
			// Don't allow external initialization (useless)
		}

		public override string ToStatusString ()
		{
			return "std";
		}
	}
}

