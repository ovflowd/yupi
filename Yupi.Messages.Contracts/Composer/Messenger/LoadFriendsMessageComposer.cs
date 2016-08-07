using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class LoadFriendsMessageComposer : AbstractComposer<Dictionary<uint, Relationship>>
	{
		public override void Compose(Yupi.Protocol.ISender session, Dictionary<uint, Relationship> friends)
		{
		 // Do nothing by default.
		}
	}
}
