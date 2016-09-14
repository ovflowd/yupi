namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class GroupForumNewThreadMessageComposer : AbstractComposer
    {
        #region Methods

        public virtual void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int habboId,
            string subject, string content, int timestamp)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}