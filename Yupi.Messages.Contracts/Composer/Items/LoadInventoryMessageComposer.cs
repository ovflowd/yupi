using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LoadInventoryMessageComposer : AbstractComposer<Inventory>
    {
        public override void Compose(ISender session, Inventory inventory)
        {
            // Do nothing by default.
        }
    }
}