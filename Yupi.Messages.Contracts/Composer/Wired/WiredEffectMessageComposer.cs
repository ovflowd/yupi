using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class WiredEffectMessageComposer : AbstractComposer<FloorItem, string, int, List<FloorItem>>
    {
        public override void Compose(Yupi.Protocol.ISender session, FloorItem item, string extraInfo, int delay,
            List<FloorItem> list = null)
        {
            // Do nothing by default.
        }
    }
}