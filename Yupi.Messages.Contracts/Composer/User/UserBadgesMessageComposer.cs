using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Linq;

namespace Yupi.Messages.Contracts
{
	public abstract class UserBadgesMessageComposer : AbstractComposer<UserInfo>
	{
		public override void Compose(Yupi.Protocol.ISender session, UserInfo user)
		{
		 // Do nothing by default.
		}
	}
}
