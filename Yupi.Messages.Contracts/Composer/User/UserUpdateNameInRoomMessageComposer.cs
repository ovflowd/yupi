using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UserUpdateNameInRoomMessageComposer : AbstractComposer<UserInfo, string>
	{
		public override void Compose(Yupi.Protocol.ISender room, UserInfo habbo, string newName)
		{
		 // Do nothing by default.
		}
	}
}
