using System;

namespace Yupi.Model.Domain
{
	public class Subscription
	{
		public virtual int Id { get; private set; }

		public virtual DateTime ExpireTime { get; }

		public virtual DateTime ActivateTime { get; private set; }

		public virtual DateTime LastGiftTime { get; private set; }

		public virtual bool IsValid ()
		{
			return ExpireTime > DateTime.Now;
		}
	}
}