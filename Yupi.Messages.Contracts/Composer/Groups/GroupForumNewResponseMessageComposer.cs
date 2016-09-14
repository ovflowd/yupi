using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumNewResponseMessageComposer : AbstractComposer
    {
        public virtual void Compose(ISender session, int groupId, int threadId, int messageCount, UserInfo user,
            int timestamp, string content)
        {
            // Do nothing by default.
        }
    }
}