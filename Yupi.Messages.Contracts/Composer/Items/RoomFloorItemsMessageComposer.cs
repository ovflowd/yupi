using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomFloorItemsMessageComposer :
        AbstractComposer<RoomData, IReadOnlyDictionary<uint, FloorItem>>
    {
        public override void Compose(ISender session, RoomData data, IReadOnlyDictionary<uint, FloorItem> items)
        {
            // Do nothing by default.
        }
    }
}