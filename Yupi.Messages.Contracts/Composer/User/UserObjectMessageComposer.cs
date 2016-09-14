namespace Yupi.Messages.Contracts
{
    using System.Globalization;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class UserObjectMessageComposer : AbstractComposer<UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}