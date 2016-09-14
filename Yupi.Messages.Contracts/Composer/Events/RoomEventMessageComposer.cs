using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomEventMessageComposer : AbstractComposer<RoomData, RoomEvent>
    {
        public override void Compose(ISender session, RoomData room, RoomEvent roomEvent)
        {
            // Do nothing by default.
        }
    }
}