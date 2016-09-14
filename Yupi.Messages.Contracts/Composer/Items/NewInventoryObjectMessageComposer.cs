using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class NewInventoryObjectMessageComposer : AbstractComposer<BaseItem, List<Item>>
    {
        public virtual void Compose(ISender session, int itemId)
        {
            // TODO Remove?
        }

        public override void Compose(ISender session, BaseItem item, List<Item> list)
        {
            // Do nothing by default.
        }
    }
}