using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UpdateFloorItemExtraDataMessageComposer : AbstractComposer<FloorItem>
	{
		public override void Compose(Yupi.Protocol.ISender room, FloorItem item)
		{
		 // Do nothing by default.
		}
	}
}
