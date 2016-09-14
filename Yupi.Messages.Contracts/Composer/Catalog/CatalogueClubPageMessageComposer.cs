using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class CatalogueClubPageMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int windowId)
		{
		 // Do nothing by default.
		}
	}
}
