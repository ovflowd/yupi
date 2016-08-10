using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class NewInventoryObjectMessageComposer : AbstractComposer<BaseItem, List<Item>>
	{
		public virtual void Compose ( Yupi.Protocol.ISender session, int itemId) {
			// TODO Remove?
		}

		public override void Compose(Yupi.Protocol.ISender session, BaseItem item, List<Item> list)
		{
		 // Do nothing by default.
		}
	}
}
