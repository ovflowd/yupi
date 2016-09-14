using System.Linq;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class RelationshipMessageComposer : AbstractComposer<UserInfo>
    {
        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
        {
            // Do nothing by default.
        }
    }
}