namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupConfirmLeaveMessageComposer : AbstractComposer<UserInfo, Group, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user, Group group, int type)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}