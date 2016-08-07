using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomFloorItemsMessageComposer : AbstractComposer <RoomData, IReadOnlyDictionary<uint, FloorItem>>
	{
		public override void Compose(Yupi.Protocol.ISender session, RoomData data, IReadOnlyDictionary<uint, FloorItem> items)
		{
		 // Do nothing by default.
		}
	}
}
