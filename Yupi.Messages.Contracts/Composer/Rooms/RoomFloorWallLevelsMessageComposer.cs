using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomFloorWallLevelsMessageComposer : AbstractComposer<RoomData>
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomData data)
        {
            // Do nothing by default.
        }
    }
}