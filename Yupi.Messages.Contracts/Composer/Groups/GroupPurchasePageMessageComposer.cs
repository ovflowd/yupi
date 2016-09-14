using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupPurchasePageMessageComposer : AbstractComposer<IList<RoomData>>
    {
        public override void Compose(Yupi.Protocol.ISender session, IList<RoomData> rooms)
        {
            // Do nothing by default.
        }
    }
}