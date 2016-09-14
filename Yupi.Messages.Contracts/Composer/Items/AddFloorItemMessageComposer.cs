using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class AddFloorItemMessageComposer : AbstractComposer<FloorItem>
	{
		public override void Compose(Yupi.Protocol.ISender room, FloorItem item)
		{
		 // Do nothing by default.
		}
	}
}
