using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class CanCreateRoomMessageComposer : AbstractComposer<UserInfo>
	{
		public override void Compose(Yupi.Protocol.ISender session, UserInfo user)
		{
		 // Do nothing by default.
		}
	}
}
