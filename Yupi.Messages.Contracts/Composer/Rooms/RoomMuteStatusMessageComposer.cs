using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomMuteStatusMessageComposer : AbstractComposer<bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, bool isMuted)
		{
		 // Do nothing by default.
		}
	}
}
