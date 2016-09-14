using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class TypingStatusMessageComposer : AbstractComposer<int, bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, int virtualId, bool isTyping)
		{
		 // Do nothing by default.
		}
	}
}
