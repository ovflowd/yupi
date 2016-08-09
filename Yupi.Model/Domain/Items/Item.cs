using System;

namespace Yupi.Model.Domain
{
	public abstract class Item : IItem {
		public virtual int Id { get; set; }
	}

	[IsDiscriminated]
	public abstract class Item<T> : Item where T : BaseItem
	{
		public virtual T BaseItem { get; set; }

		public virtual string GetExtraData() {
			return string.Empty;
		}
	}
}

