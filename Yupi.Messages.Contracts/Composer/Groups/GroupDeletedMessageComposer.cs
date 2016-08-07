using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class GroupDeletedMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender room, int groupId)
		{
		 // Do nothing by default.
		}
	}
}
