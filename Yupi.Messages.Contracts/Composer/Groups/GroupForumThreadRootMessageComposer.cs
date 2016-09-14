using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumThreadRootMessageComposer : AbstractComposer<int, int, IList<GroupForumThread>>
    {
        public override void Compose(ISender session, int groupId, int startIndex, IList<GroupForumThread> threads)
        {
            // Do nothing by default.
        }
    }
}