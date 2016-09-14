using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OfficialRoomsMessageComposer : AbstractComposer<RoomData>
    {
        public override void Compose(ISender session, RoomData roomData)
        {
            // Do nothing by default.
        }
    }
}