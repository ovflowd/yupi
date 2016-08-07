using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class CatalogPromotionGetRoomsMessageComposer : AbstractComposer<HashSet<RoomData>>
	{
		public override void Compose(Yupi.Protocol.ISender session, HashSet<RoomData> rooms)
		{
		 // Do nothing by default.
		}
	}
}
