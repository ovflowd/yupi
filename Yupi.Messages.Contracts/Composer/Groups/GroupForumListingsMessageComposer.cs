using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupForumListingsMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int selectType, int startIndex)
        {
            // Do nothing by default.
        }
    }
}