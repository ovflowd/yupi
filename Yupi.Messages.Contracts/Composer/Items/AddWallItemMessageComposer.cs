using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class AddWallItemMessageComposer : AbstractComposer<WallItem, UserInfo>
    {
        public override void Compose(Yupi.Protocol.ISender session, WallItem item, UserInfo user)
        {
            // Do nothing by default.
        }
    }
}