namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupForumThreadUpdateMessageComposer : AbstractComposer
    {
        #region Methods

        public virtual void Compose(Yupi.Protocol.ISender session, int groupId, GroupForumThread thread, bool pin,
            bool Lock)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}