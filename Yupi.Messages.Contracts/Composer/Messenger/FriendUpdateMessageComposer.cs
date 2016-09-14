using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class FriendUpdateMessageComposer : AbstractComposer<Relationship>
    {
        public override void Compose(Yupi.Protocol.ISender session, Relationship relationship)
        {
            // Do nothing by default.
        }
    }
}