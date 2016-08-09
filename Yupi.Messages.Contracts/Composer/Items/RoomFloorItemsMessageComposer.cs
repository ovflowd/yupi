using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomFloorItemsMessageComposer : AbstractComposer <RoomData, IReadOnlyDictionary<uint, IFloorItem>>
	{
		public override void Compose(Yupi.Protocol.ISender session, RoomData data, IReadOnlyDictionary<uint, IFloorItem> items)
		{
		 // Do nothing by default.
		}
	}
}
