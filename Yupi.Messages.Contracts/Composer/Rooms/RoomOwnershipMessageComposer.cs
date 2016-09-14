using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomOwnershipMessageComposer : AbstractComposer<RoomData, UserInfo>
    {
        public override void Compose(ISender session, RoomData room, UserInfo user)
        {
            // Do nothing by default.
        }
    }
}