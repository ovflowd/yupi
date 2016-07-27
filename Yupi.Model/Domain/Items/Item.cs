using System;

namespace Yupi.Model.Domain
{
	[IsDiscriminated]
	public abstract class Item<T> where T : BaseItem
	{
		public virtual int Id { get; set; }

		public virtual T BaseItem { get; set; }

		public virtual string GetExtraData() {
			return string.Empty;
		}
	}
}

