using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class ConsoleChatMessageComposer : AbstractComposer<uint, string, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint converstationId, string text, int timeDiff = 0)
		{
		 // Do nothing by default.
		}
	}
}
