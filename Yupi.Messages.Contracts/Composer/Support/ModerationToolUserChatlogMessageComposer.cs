using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class ModerationToolUserChatlogMessageComposer : AbstractComposer<uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint userId)
		{
		 // Do nothing by default.
		}
	}
}
