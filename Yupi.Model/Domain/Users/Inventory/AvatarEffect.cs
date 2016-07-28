using System;

namespace Yupi.Model.Domain
{
	// TODO Should this be written to DB?
	[Ignore]
	public class AvatarEffect
	{
		public bool Activated;

		/// <summary>
		///     The effect identifier
		/// </summary>
		public int EffectId;

		public DateTime ActivatedAt;

		/// <summary>
		///     The total duration
		/// </summary>
		public int TotalDuration;

		/// <summary>
		///     The type
		/// </summary>
		public short Type;

		public int TimeLeft {
			get {
				if (!Activated || TotalDuration == -1)
					return -1;

				double remaining = (DateTime.Now - ActivatedAt).TotalSeconds;

				if (remaining >= TotalDuration)
					return 0;

				return (int)(TotalDuration - remaining);
			}
		}

		public bool HasExpired ()
		{
			return TimeLeft != -1 && TimeLeft <= 0;
		}

		public void Activate ()
		{
			Activated = true;
			ActivatedAt = DateTime.Now;
		}
	}
}