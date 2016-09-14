using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class GroupRoomMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int roomId, int groupId)
		{
		 // Do nothing by default.
		}
	}
}
