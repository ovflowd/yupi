using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomDataMessageComposer : AbstractComposer<RoomData, UserInfo, bool, bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, RoomData room, UserInfo user, bool show, bool isNotReload)
		{
		 // Do nothing by default.
		}
	}
}
