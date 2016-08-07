using Yupi.Protocol.Buffers;
using System.Linq;

namespace Yupi.Messages.Contracts
{
	public abstract class ModerationToolRoomVisitsMessageComposer : AbstractComposer<uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint userId)
		{
		 // Do nothing by default.
		}
	}
}
