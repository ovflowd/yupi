using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class WiredEffectMessageComposer : AbstractComposer<FloorItem, string, int, List<FloorItem>>
    {
        public override void Compose(ISender session, FloorItem item, string extraInfo, int delay,
            List<FloorItem> list = null)
        {
            // Do nothing by default.
        }
    }
}