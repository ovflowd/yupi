using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ChangeFavouriteGroupMessageComposer : AbstractComposer<Group, int>
    {
        public override void Compose(ISender session, Group group, int virtualId)
        {
            // Do nothing by default.
        }
    }
}