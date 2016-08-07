using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class NewInventoryObjectMessageComposer : AbstractComposer<BaseItem, List<UserItem>>
	{
		public virtual void Compose ( Yupi.Protocol.ISender session, uint itemId) {
			// TODO Remove?
		}

		public override void Compose(Yupi.Protocol.ISender session, BaseItem item, List<UserItem> list)
		{
		 // Do nothing by default.
		}
	}
}
