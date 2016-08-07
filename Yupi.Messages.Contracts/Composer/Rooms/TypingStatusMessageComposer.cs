using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class TypingStatusMessageComposer : AbstractComposer<uint, bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint virtualId, bool isTyping)
		{
		 // Do nothing by default.
		}
	}
}
