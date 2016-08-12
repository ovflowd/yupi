using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ConsoleSendFriendRequestMessageComposer : AbstractComposer<FriendRequest>
	{
		public override void Compose(Yupi.Protocol.ISender session, FriendRequest request)
		{
		 // Do nothing by default.
		}
	}
}
