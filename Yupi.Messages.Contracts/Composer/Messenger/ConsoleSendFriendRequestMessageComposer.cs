using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ConsoleSendFriendRequestMessageComposer : AbstractComposer<MessengerRequest>
	{
		public override void Compose(Yupi.Protocol.ISender session, MessengerRequest request)
		{
		 // Do nothing by default.
		}
	}
}
