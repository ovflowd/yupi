namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupForumDataMessageComposer : AbstractComposer<Group, UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Group group, UserInfo user)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}