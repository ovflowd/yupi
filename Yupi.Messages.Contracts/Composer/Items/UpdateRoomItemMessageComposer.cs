using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UpdateRoomItemMessageComposer : AbstractComposer<IFloorItem>
	{
		public override void Compose(Yupi.Protocol.ISender session, IFloorItem item)
		{
		 // Do nothing by default.
		}
	}
}
