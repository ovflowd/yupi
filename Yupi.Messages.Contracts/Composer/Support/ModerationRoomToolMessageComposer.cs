using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ModerationRoomToolMessageComposer : AbstractComposer<RoomData, bool>
    {
        public override void Compose(ISender session, RoomData data, bool isLoaded)
        {
            // Do nothing by default.
        }
    }
}