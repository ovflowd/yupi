using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UsersClassificationMessageComposer : AbstractComposer<UserInfo, string>
	{
		public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo, string word)
		{
		 // Do nothing by default.
		}
	}
}
