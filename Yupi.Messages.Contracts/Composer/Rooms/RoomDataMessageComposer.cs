using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomDataMessageComposer : AbstractComposer<RoomData, bool, bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, RoomData room, bool show, bool isNotReload)
		{
		 // Do nothing by default.
		}
	}
}
