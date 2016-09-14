using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumNewThreadMessageComposer : AbstractComposer
    {
        public virtual void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int habboId,
            string subject, string content, int timestamp)
        {
            // Do nothing by default.
        }
    }
}