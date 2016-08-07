using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Messages.Contracts
{
	public abstract class ModerationToolRoomChatlogMessageComposer : AbstractComposer<uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint roomId)
		{
		 // Do nothing by default.
		}
	}
}
