using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class GiveRoomRightsMessageComposer : AbstractComposer<int, UserInfo>
	{
		public override void Compose(Yupi.Protocol.ISender session, int roomId, UserInfo habbo)
		{
		 // Do nothing by default.
		}
	}
}
