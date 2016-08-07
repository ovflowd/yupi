using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class ChatMessageComposer : AbstractComposer<uint, string, int, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint entityId, string msg, int color, int count = 0)
		{
		 // Do nothing by default.
		}
	}
}
