using System;

namespace Yupi.Model.Domain
{
	[IsDiscriminated]
	public abstract class Item {
		public virtual int Id { get; set; }
	}
}

