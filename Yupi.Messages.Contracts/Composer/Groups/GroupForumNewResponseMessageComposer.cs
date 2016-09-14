namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupForumNewResponseMessageComposer : AbstractComposer
    {
        #region Methods

        public virtual void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int messageCount,
            UserInfo user, int timestamp, string content)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}