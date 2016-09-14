using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class FollowFriendErrorMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int status)
		{
		 // Do nothing by default.
		}
	}
}
