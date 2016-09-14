using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GiveRoomRightsMessageComposer : AbstractComposer<int, UserInfo>
    {
        public override void Compose(ISender session, int roomId, UserInfo habbo)
        {
            // Do nothing by default.
        }
    }
}