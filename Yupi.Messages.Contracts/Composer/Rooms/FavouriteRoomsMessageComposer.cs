using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class FavouriteRoomsMessageComposer : AbstractComposer<List<uint>>
	{
		public override void Compose(Yupi.Protocol.ISender session, List<uint> rooms)
		{
		 // Do nothing by default.
		}
	}
}
