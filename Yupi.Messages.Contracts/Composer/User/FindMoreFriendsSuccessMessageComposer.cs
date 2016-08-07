using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class FindMoreFriendsSuccessMessageComposer : AbstractComposer<bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, bool success)
		{
		 // Do nothing by default.
		}
	}
}
