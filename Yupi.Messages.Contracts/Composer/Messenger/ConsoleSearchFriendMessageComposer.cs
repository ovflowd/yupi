namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class ConsoleSearchFriendMessageComposer : AbstractComposer<List<UserInfo>, List<UserInfo>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, List<UserInfo> foundFriends,
            List<UserInfo> foundUsers)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}