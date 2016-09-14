namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class UserProfileMessageComposer : AbstractComposer<UserInfo, UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo, UserInfo requester)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}