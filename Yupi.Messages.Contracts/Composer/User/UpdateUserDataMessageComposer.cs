namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class UpdateUserDataMessageComposer : AbstractComposer<UserInfo, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, UserInfo habbo, int roomUserId = -1)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}