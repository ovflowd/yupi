using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class OnGuideSessionInvitedToGuideRoomMessageComposer : AbstractComposer<int, string>
	{
		public override void Compose(Yupi.Protocol.ISender session, int roomId, string roomName)
		{
		 // Do nothing by default.
		}
	}
}
