namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupForumThreadRootMessageComposer : AbstractComposer<int, int, IList<GroupForumThread>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int groupId, int startIndex,
            IList<GroupForumThread> threads)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}