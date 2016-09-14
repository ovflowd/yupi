namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupForumReadThreadMessageComposer : AbstractComposer
    {
        #region Methods

        public virtual void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int startIndex,
            List<GroupForumPost> posts)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}