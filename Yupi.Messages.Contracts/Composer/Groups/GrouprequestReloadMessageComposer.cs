using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class GrouprequestReloadMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int groupId)
		{
		 // Do nothing by default.
		}
	}
}
