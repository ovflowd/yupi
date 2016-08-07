using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class RecyclerRewardsMessageComposer : AbstractComposerVoid
	{
		public override void Compose(Yupi.Protocol.ISender session)
		{
		 // Do nothing by default.
		}
	}
}
