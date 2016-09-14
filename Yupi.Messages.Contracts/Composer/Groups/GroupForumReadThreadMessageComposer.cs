using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumReadThreadMessageComposer : AbstractComposer
    {
        public virtual void Compose(ISender session, int groupId, int threadId, int startIndex,
            List<GroupForumPost> posts)
        {
            // Do nothing by default.
        }
    }
}