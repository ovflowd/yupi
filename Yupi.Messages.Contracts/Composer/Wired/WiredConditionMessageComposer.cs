using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class WiredConditionMessageComposer : AbstractComposer<IFloorItem, List<IFloorItem>, string>
	{
		public override void Compose(Yupi.Protocol.ISender session, IFloorItem item, List<IFloorItem> list, string extraString)
		{
		 // Do nothing by default.
		}
	}
}
