using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class WiredConditionMessageComposer : AbstractComposer<FloorItem, List<FloorItem>, string>
	{
		public override void Compose(Yupi.Protocol.ISender session, FloorItem item, List<FloorItem> list, string extraString)
		{
		 // Do nothing by default.
		}
	}
}
