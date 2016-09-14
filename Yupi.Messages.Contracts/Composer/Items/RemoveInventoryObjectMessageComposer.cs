using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public class RemoveInventoryObjectMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int itemId)
        {
        }
    }
}