using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class AddWallItemMessageComposer : AbstractComposer<WallItem, UserInfo>
    {
        public override void Compose(ISender session, WallItem item, UserInfo user)
        {
            // Do nothing by default.
        }
    }
}