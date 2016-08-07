using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UpdateUserDataMessageComposer : AbstractComposer<UserInfo, int>
	{
		public override void Compose(Yupi.Protocol.ISender room, UserInfo habbo, int roomUserId = -1)
		{
		 // Do nothing by default.
		}
	}
}
