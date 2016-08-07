using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomBannedListMessageComposer : AbstractComposer<uint, List<uint>>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint roomId, List<uint> bannedUsers)
		{
		 // Do nothing by default.
		}
	}
}
