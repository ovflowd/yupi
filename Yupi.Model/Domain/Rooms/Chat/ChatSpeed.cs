using System;
using Headspring;

namespace Yupi.Model.Domain
{
	public class ChatSpeed : Enumeration<ChatSpeed> {
		public static readonly ChatSpeed Fast = new ChatSpeed(0, "Fast");
		public static readonly ChatSpeed Normal = new ChatSpeed(1, "Normal");
		public static readonly ChatSpeed Slow = new ChatSpeed(2, "Slow");

		private ChatSpeed (int value, string displayName) : base (value, displayName)
		{
		}
	}
}

