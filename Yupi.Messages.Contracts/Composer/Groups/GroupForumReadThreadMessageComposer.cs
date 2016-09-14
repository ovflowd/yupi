using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumReadThreadMessageComposer : AbstractComposer
    {
        public virtual void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int startIndex,
            List<GroupForumPost> posts)
        {
            // Do nothing by default.
        }
    }
}