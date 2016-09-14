using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Messages.Contracts
{
    public abstract class FigureSetIdsMessageComposer : AbstractComposerVoid
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            // Do nothing by default.
        }
    }
}