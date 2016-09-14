using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ConsoleSearchFriendMessageComposer : AbstractComposer<List<UserInfo>, List<UserInfo>>
    {
        public override void Compose(ISender session, List<UserInfo> foundFriends, List<UserInfo> foundUsers)
        {
            // Do nothing by default.
        }
    }
}