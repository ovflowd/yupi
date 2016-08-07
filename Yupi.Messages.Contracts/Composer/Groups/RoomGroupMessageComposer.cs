using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomGroupMessageComposer : AbstractComposerVoid
	{
		public override void Compose(Yupi.Protocol.ISender room)
		{
		 // Do nothing by default.
		}
	}
}
