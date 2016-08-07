using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomUserIdleMessageComposer : AbstractComposer<uint, bool>
	{
		public override void Compose(Yupi.Protocol.ISender room, uint entityId, bool isAsleep)
		{
		 // Do nothing by default.
		}
	}
}
