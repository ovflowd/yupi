using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ModerationRoomToolMessageComposer : AbstractComposer<RoomData, bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, RoomData data, bool isLoaded)
		{
		 // Do nothing by default.
		}
	}
}
