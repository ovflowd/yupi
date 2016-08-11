using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ModerationToolRoomChatlogMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose(Yupi.Protocol.ISender session, RoomData room)
		{
		 // Do nothing by default.
		}
	}
}
