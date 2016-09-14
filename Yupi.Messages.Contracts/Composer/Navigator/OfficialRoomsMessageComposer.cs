using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class OfficialRoomsMessageComposer : AbstractComposer<RoomData>
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomData roomData)
        {
            // Do nothing by default.
        }
    }
}