using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomRightsLevelMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int level)
		{
		 // Do nothing by default.
		}
	}
}
