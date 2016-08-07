using System;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class PurchaseOkComposer : AbstractComposer
	{
		public virtual void Compose( Yupi.Protocol.ISender session, CatalogItem itemCatalog, Dictionary<BaseItem, uint> items,
			int clubLevel = 1) {
			// Do nothing by default.
		}
	}
}

