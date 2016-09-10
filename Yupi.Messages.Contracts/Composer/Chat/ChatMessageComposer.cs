using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ChatMessageComposer : AbstractComposer<ChatlogEntry, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, ChatlogEntry msg, int count = -1)
		{
		 // Do nothing by default.
		}
	}
}
