using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomOwnershipMessageComposer : AbstractComposer<RoomData, UserInfo>
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomData room, UserInfo user)
        {
            // Do nothing by default.
        }
    }
}