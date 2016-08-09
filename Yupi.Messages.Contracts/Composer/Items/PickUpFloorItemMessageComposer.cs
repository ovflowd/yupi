using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class PickUpFloorItemMessageComposer : AbstractComposer<IFloorItem, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, IFloorItem item, int pickerId)
		{
		 // Do nothing by default.
		}
	}
}
