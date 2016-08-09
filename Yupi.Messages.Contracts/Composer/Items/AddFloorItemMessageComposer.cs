using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class AddFloorItemMessageComposer : AbstractComposer<IFloorItem>
	{
		public override void Compose(Yupi.Protocol.ISender room, IFloorItem item)
		{
		 // Do nothing by default.
		}
	}
}
