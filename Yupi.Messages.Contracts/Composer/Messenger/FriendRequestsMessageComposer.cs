using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class FriendRequestsMessageComposer : AbstractComposer<IDictionary<uint, MessengerRequest>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IDictionary<uint, MessengerRequest> requests)
		{
		 // Do nothing by default.
		}
	}
}
