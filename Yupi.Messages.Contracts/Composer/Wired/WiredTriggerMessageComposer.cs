using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class WiredTriggerMessageComposer : AbstractComposer
	{
		public virtual void Compose(Yupi.Protocol.ISender session, IFloorItem item, List<IFloorItem> items, int delay, string extraInfo, int unknown)
		{
		 // Do nothing by default.
		}
	}
}
