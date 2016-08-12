using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class FriendRequestsMessageComposer : AbstractComposer<IDictionary<uint, FriendRequest>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IDictionary<uint, FriendRequest> requests)
		{
		 // Do nothing by default.
		}
	}
}
