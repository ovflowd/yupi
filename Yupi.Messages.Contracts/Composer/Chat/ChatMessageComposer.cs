using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ChatMessageComposer : AbstractComposer<int, string, ChatBubbleStyle, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int entityId, string msg, ChatBubbleStyle color, int count = 0)
		{
		 // Do nothing by default.
		}
	}
}
