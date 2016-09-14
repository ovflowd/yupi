using Yupi.Protocol.Buffers;
using Yupi.Net;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class UserTagsMessageComposer : AbstractComposer<UserInfo>
    {
        public override void Compose(Yupi.Protocol.ISender session, UserInfo info)
        {
            // Do nothing by default.
        }
    }
}