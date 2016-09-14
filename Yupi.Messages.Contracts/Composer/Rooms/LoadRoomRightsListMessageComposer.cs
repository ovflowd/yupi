using Yupi.Protocol.Buffers;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class LoadRoomRightsListMessageComposer : AbstractComposer<RoomData>
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomData room)
        {
            // Do nothing by default.
        }
    }
}