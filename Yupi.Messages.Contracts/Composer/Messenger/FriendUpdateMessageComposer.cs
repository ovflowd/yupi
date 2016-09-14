using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class FriendUpdateMessageComposer : AbstractComposer<Relationship>
    {
        public override void Compose(ISender session, Relationship relationship)
        {
            // Do nothing by default.
        }
    }
}