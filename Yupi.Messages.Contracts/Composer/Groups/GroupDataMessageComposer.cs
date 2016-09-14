namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupDataMessageComposer : AbstractComposer<Group, UserInfo, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Group group, UserInfo habbo, bool newWindow = false)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}