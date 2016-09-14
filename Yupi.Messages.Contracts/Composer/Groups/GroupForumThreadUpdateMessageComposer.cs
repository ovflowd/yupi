using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumThreadUpdateMessageComposer : AbstractComposer
    {
        public virtual void Compose(Yupi.Protocol.ISender session, int groupId, GroupForumThread thread, bool pin,
            bool Lock)
        {
            // Do nothing by default.
        }
    }
}