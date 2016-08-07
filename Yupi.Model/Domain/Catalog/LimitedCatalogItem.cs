using System;

namespace Yupi.Model.Domain
{
	public class LimitedCatalogItem : CatalogItem
	{
		public virtual int LimitedStack { get; protected set; }

		public virtual int LimitedSelled { get; protected set; }
	}
}

