using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class WiredConditionMessageComposer : AbstractComposer<FloorItem, List<FloorItem>, string>
    {
        public override void Compose(ISender session, FloorItem item, List<FloorItem> list, string extraString)
        {
            // Do nothing by default.
        }
    }
}