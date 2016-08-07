using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomUserActionMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose(Yupi.Protocol.ISender room, int virtualId, int unknown = 7)
		{
		 // Do nothing by default.
		}
	}
}
