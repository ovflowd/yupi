using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class NavigatorLiftedRoomsComposer : AbstractComposerVoid
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            // Do nothing by default.
        }
    }
}