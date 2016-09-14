using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class FriendRequestsMessageComposer : AbstractComposer<IList<FriendRequest>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<FriendRequest> requests)
		{
		 // Do nothing by default.
		}
	}
}
