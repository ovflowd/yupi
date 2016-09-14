namespace Yupi.Messages.Contracts
{
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public abstract class UserBadgesMessageComposer : AbstractComposer<UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}