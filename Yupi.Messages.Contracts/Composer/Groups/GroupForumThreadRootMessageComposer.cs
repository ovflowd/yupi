using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumThreadRootMessageComposer : AbstractComposer<int, int, IList<GroupForumThread>>
    {
        public override void Compose(Yupi.Protocol.ISender session, int groupId, int startIndex,
            IList<GroupForumThread> threads)
        {
            // Do nothing by default.
        }
    }
}