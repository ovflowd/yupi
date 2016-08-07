using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class GiveRoomRightsMessageComposer : AbstractComposer<uint, UserInfo>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint roomId, UserInfo habbo)
		{
		 // Do nothing by default.
		}
	}
}
