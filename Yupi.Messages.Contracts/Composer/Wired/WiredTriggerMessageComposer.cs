using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class WiredTriggerMessageComposer : AbstractComposer
    {
        public virtual void Compose(ISender session, FloorItem item, List<FloorItem> items, int delay, string extraInfo,
            int unknown)
        {
            // Do nothing by default.
        }
    }
}