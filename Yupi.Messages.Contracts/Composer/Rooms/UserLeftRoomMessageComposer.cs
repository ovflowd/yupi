using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class UserLeftRoomMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int virtualId)
		{
		 // Do nothing by default.
		}
	}
}
