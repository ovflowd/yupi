using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ConsoleSearchFriendMessageComposer : AbstractComposer<List<UserInfo>, List<UserInfo>>
	{
		public override void Compose(Yupi.Protocol.ISender session, List<UserInfo> foundFriends, List<UserInfo> foundUsers)
		{
		 // Do nothing by default.
		}
	}
}
