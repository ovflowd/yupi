using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CatalogPromotionGetRoomsMessageComposer : AbstractComposer<IList<RoomData>>
    {
        public override void Compose(ISender session, IList<RoomData> rooms)
        {
            // Do nothing by default.
        }
    }
}