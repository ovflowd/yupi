using System;

namespace Yupi.Model.Domain
{
	public class Subscription
	{
		public virtual int Id { get; protected set; }

		public virtual DateTime ExpireTime { get; set; }

		public virtual DateTime ActivateTime { get; set; }

		public virtual DateTime LastGiftTime { get; set; }

		public virtual bool IsValid ()
		{
			return ExpireTime > DateTime.Now;
		}
	}
}