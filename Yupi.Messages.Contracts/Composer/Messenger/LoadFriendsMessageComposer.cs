using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LoadFriendsMessageComposer : AbstractComposer<IList<Relationship>>
    {
        public override void Compose(ISender session, IList<Relationship> friends)
        {
            // Do nothing by default.
        }
    }
}