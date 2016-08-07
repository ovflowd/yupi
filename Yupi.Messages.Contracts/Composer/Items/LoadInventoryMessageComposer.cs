using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using System.Globalization;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class LoadInventoryMessageComposer : AbstractComposer<ICollection<UserItem>, ICollection<UserItem>, ICollection<UserItem>>
	{
		public override void Compose(Yupi.Protocol.ISender session, ICollection<UserItem> floor, ICollection<UserItem> wall, ICollection<UserItem> songDisks)
		{
		 // Do nothing by default.
		}
	}
}
