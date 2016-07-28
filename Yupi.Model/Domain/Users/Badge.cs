using System;

namespace Yupi.Model.Domain
{
	public class Badge
	{
		public virtual int Id { get; protected set; }
		public virtual int Slot { get; set; }
		public virtual string Code { get; set; }
	}
}

