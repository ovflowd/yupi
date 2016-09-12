using System;
using Headspring;

namespace Yupi.Model.Domain
{
	public class FloodProtection : Enumeration<FloodProtection> {
		public static readonly FloodProtection Extra = new FloodProtection(0, "Extra");
		public static readonly FloodProtection Standard = new FloodProtection(1, "Standard");
		public static readonly FloodProtection Minimal = new FloodProtection(2, "Minimal");

		private FloodProtection (int value, string displayName) : base (value, displayName)
		{
		}
	}
}

