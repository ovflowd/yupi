using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomBannedListMessageComposer : AbstractComposer<RoomData>
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomData room)
        {
            // Do nothing by default.
        }
    }
}