using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumThreadUpdateMessageComposer : AbstractComposer
    {
        public virtual void Compose(ISender session, int groupId, GroupForumThread thread, bool pin, bool Lock)
        {
            // Do nothing by default.
        }
    }
}