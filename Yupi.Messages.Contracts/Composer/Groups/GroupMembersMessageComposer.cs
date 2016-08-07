using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class GroupMembersMessageComposer : AbstractComposer<UserInfo, Group>
	{
		public override void Compose(Yupi.Protocol.ISender session, UserInfo user, Group group)
		{
		 // Do nothing by default.
		}
		public virtual void Compose(Yupi.Protocol.ISender session, Group group, uint reqType, UserInfo user, string searchVal = "", int page = 0)
		{
		 // Do nothing by default.
		}
	}
}
