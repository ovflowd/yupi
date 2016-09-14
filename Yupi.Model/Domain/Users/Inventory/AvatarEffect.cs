using System;

namespace Yupi.Model.Domain
{
	public class AvatarEffect
	{
		public virtual int Id { get; set; }

		public virtual int EffectId { get; set; }

		// TODO Redundant information (UserEffectComponent.ActiveEffect)
		public virtual bool Activated { get; set; }

		public virtual DateTime ActivatedAt { get; set; }

		public virtual int TotalDuration { get; set; }

		// TODO What is this good for?
		public virtual short Type { get; set; }

		public virtual int TimeLeft() {
				if (!Activated || TotalDuration == -1)
					return -1;

				double remaining = (DateTime.Now - ActivatedAt).TotalSeconds;

				if (remaining >= TotalDuration)
					return 0;

				return (int)(TotalDuration - remaining);
		}

		public virtual bool HasExpired ()
		{
			return TimeLeft() != -1 && TimeLeft() <= 0;
		}

		public virtual void Activate ()
		{
			Activated = true;
			ActivatedAt = DateTime.Now;
		}
	}
}