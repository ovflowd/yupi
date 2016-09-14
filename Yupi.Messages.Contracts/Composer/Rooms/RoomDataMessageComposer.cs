using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomDataMessageComposer : AbstractComposer<RoomData, UserInfo, bool, bool>
    {
        public override void Compose(ISender session, RoomData room, UserInfo user, bool show, bool isNotReload)
        {
            // Do nothing by default.
        }
    }
}