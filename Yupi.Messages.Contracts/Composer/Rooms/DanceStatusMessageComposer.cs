using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class DanceStatusMessageComposer : AbstractComposer<uint, uint>
	{
		public override void Compose(Yupi.Protocol.ISender room, uint entityId, uint danceId)
		{
		 // Do nothing by default.
		}
	}
}
