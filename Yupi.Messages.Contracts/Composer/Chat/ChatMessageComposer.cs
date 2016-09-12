using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ChatMessageComposer : AbstractComposer<ChatMessage, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, ChatMessage msg, int count = -1)
		{
		 // Do nothing by default.
		}
	}
}
