namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class UsersClassificationMessageComposer : AbstractComposer<UserInfo, string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo, string word)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}