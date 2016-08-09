using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UpdateFloorItemExtraDataMessageComposer : AbstractComposer<IFloorItem>
	{
		public override void Compose(Yupi.Protocol.ISender room, IFloorItem item)
		{
		 // Do nothing by default.
		}
	}
}
