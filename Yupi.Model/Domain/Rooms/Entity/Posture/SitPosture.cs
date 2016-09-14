using System;
using System.Globalization;

namespace Yupi.Model
{
	public class SitPosture : EntityPosture
	{
		// TODO What are valid values for deltaZ?
		private float DeltaZ;
		private bool CanStandUp;

		public static readonly SitPosture Default = new SitPosture (0.55f);

		public SitPosture (float deltaZ, bool canStandUp = true)
		{
			this.DeltaZ = deltaZ;
			this.CanStandUp = canStandUp;
		}

		public override string ToStatusString ()
		{
			return "sit " + this.DeltaZ.ToString (CultureInfo.InvariantCulture) + " " + Convert.ToInt32 (this.CanStandUp);
		}
	}
}

