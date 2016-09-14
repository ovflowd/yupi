using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomGroupMessageComposer : AbstractComposer<ISet<Group>>
    {
        public override void Compose(Yupi.Protocol.ISender room, ISet<Group> groups)
        {
            // Do nothing by default.
        }
    }
}